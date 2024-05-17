using System.Globalization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Toolkit.Extensions.Authentication.Abstractions;
using Toolkit.Extensions.Clients.Http;
using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Authentication.OpenIdConnect;

/// <summary>
///	Keycloak service account client.
/// </summary>
/// <param name="logger">Logger for current instance.</param>
/// <param name="httpClient">Http client for requesting data.</param>
/// <param name="httpResponseParser">Response parser service.</param>
/// <param name="configuration"></param>
/// <param name="urlProvider"></param>
internal sealed class KeycloakAuthenticationService(
	ILogger<KeycloakAuthenticationService> logger,
	HttpClient httpClient,
	IHttpResponseParser httpResponseParser,
	IOptions<OpenIdConnectServiceConnectionOptions> configuration,
	IOpenIdConnectConfigurationProvider urlProvider)
	: HttpServiceClient(logger, httpClient, httpResponseParser), IOpenIdConnectAuthenticationService
{
	/// <inheritdoc/>
	public async ValueTask<OperationContext<string>> GetLoginPageUrlAsync(Uri redirectUri, string state, CancellationToken cancellationToken = default)
	{
		var result = new OperationContext<string>();

		OpenIdConnectConfiguration openIdConfiguration = await urlProvider.GetConfigurationAsync(configuration.Value.Endpoint, configuration.Value.Realm, cancellationToken)
			.ConfigureAwait(false);
		string loginRedirectUrl = string.Format(
			CultureInfo.InvariantCulture,
			KeycloakAuthenticationOperation.GetLoginUrl.GetDescription(),
			openIdConfiguration.AuthorizationEndpoint,
			configuration.Value.RequestedResponseType,
			configuration.Value.Authentication?.UserId,
			redirectUri.ToString(),
			state);

		return result.SetResult(loginRedirectUrl);
	}

	/// <inheritdoc/>
	public ValueTask<OperationContext<OpenIdConnectTokenResponseModel?>> ExchangeCodeForTokenAsync(Uri redirectUri, string code, CancellationToken cancellationToken = default)
		=> RequestTokenAsync(OpenIdConnectGrantTypes.AuthorizationCode, redirectUri: redirectUri, code: code, cancellationToken: cancellationToken);

	/// <inheritdoc/>
	public ValueTask<OperationContext<OpenIdConnectTokenResponseModel?>> ExchangeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
		=> RequestTokenAsync(OpenIdConnectGrantTypes.RefreshToken, refreshToken: refreshToken, cancellationToken: cancellationToken);

	/// <inheritdoc/>
	public ValueTask<OperationContext<OpenIdConnectTokenResponseModel?>> ExchangeUserPasswordForTokenAsync(string username, string password, CancellationToken cancellationToken = default)
		=> RequestTokenAsync(OpenIdConnectGrantTypes.Password, username: username, password: password, cancellationToken: cancellationToken);

	/// <inheritdoc/>
	public async ValueTask<OperationContext<OpenIdConnectTokenResponseModel?>> RequestTokenAsync(
		string grantType,
		string? username = null,
		string? password = null,
		string? refreshToken = null,
		Uri? redirectUri = null,
		string? code = null,
		CancellationToken cancellationToken = default)
	{
		if (configuration.Value.Authentication == null)
		{
			throw new InvalidOperationException("Missing AuthorizationPolicy");
    }

		OpenIdConnectConfiguration requestUrlConfiguration = await urlProvider.GetConfigurationAsync(configuration.Value.Endpoint, configuration.Value.Realm, cancellationToken)
			.ConfigureAwait(false);

		var tokenRequestContent = new Dictionary<string, string>
		{
			{ OpenIdConnectParameterNames.GrantType, grantType },
			{ OpenIdConnectParameterNames.Scope, OpenIdConnectScope.OpenIdProfile }
		};

		switch (grantType)
		{
			case OpenIdConnectGrantTypes.AuthorizationCode:
			{
				ArgumentNullException.ThrowIfNull(redirectUri);
				ArgumentException.ThrowIfNullOrWhiteSpace(code);

				tokenRequestContent.Add(OpenIdConnectParameterNames.RedirectUri, redirectUri.ToString());
				tokenRequestContent.Add(OpenIdConnectParameterNames.Code, code);
			} break;
			case OpenIdConnectGrantTypes.RefreshToken:
			{
				ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);

				tokenRequestContent.Add(OpenIdConnectParameterNames.RefreshToken, refreshToken);
			} break;
			case OpenIdConnectGrantTypes.Password:
			{
				ArgumentException.ThrowIfNullOrWhiteSpace(username);
				ArgumentException.ThrowIfNullOrWhiteSpace(password);

				tokenRequestContent.Add(OpenIdConnectParameterNames.Username, username);
				tokenRequestContent.Add(OpenIdConnectParameterNames.Password, password);
			} break;
		}

		// In version 24 used another type of client authentication
		if (configuration.Value.ServiceType == IdentityServiceType.Keycloak_v18)
		{
			tokenRequestContent.Add(OpenIdConnectParameterNames.ClientId, configuration.Value.Authentication.UserId);
			tokenRequestContent.Add(OpenIdConnectParameterNames.ClientSecret, configuration.Value.Authentication.UserSecret);
		}

		using var message = new HttpRequestMessage(HttpMethod.Post, requestUrlConfiguration.TokenEndpoint)
		{
			Content = new FormUrlEncodedContent(tokenRequestContent)
		};

		return await ExecuteHttpRequestAsync<OpenIdConnectTokenResponseModel>(message, cancellationToken)
			.ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async ValueTask<OperationContext> LogoutAsync(
		string refreshToken,
		Uri redirectUri,
		string state,
		CancellationToken cancellationToken = default)
	{
		if (configuration.Value.Authentication == null)
		{
			throw new InvalidOperationException("Missing AuthorizationPolicy");
		}

		OpenIdConnectConfiguration requestUrlConfig = await urlProvider.GetConfigurationAsync(configuration.Value.Endpoint, configuration.Value.Realm, cancellationToken)
			.ConfigureAwait(false);
		string logoutUrl = string.Format(
			CultureInfo.InvariantCulture,
			KeycloakAuthenticationOperation.GetLogOutUrl.GetDescription(),
			requestUrlConfig.EndSessionEndpoint,
			redirectUri.ToString(),
			state);

		using var message = new HttpRequestMessage(HttpMethod.Post, logoutUrl)
		{
			Content = new FormUrlEncodedContent(
				new Dictionary<string, string>
				{
					{ OpenIdConnectParameterNames.RefreshToken, refreshToken },
					{ OpenIdConnectParameterNames.ClientId, configuration.Value.Authentication.UserId },
					{ OpenIdConnectParameterNames.ClientSecret, configuration.Value.Authentication.UserSecret }
				}
			)
		};

		return await ExecuteHttpRequestAsync(message, cancellationToken)
			.ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public async ValueTask<OperationContext> RevokeClientSessionAsync(string refreshToken, CancellationToken cancellationToken = default)
	{
		if (configuration.Value.Authentication == null)
		{
			throw new InvalidOperationException("Missing AuthorizationPolicy");
		}

		OpenIdConnectConfiguration requestUrlConfig = await urlProvider.GetConfigurationAsync(configuration.Value.Endpoint, configuration.Value.Realm, cancellationToken)
			.ConfigureAwait(false);

		using var message = new HttpRequestMessage(
			HttpMethod.Post,
			requestUrlConfig.AdditionalData["revocation_endpoint"].ToString())
		{
			Content = new FormUrlEncodedContent(
				new Dictionary<string, string>
				{
					{ KeycloakOpenIdConnectParameterNames.Token, refreshToken },
					{ OpenIdConnectParameterNames.ClientId, configuration.Value.Authentication.UserId },
					{ OpenIdConnectParameterNames.ClientSecret, configuration.Value.Authentication.UserSecret },
					{ KeycloakOpenIdConnectParameterNames.TokenType, OpenIdConnectParameterNames.RefreshToken }
				}
			)
		};

		return await ExecuteHttpRequestAsync(message, cancellationToken)
			.ConfigureAwait(false);
	}
}
