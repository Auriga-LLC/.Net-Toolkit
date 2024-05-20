using Keycloak.Plugin.Abstractions.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Net.Http.Headers;
using Auriga.Toolkit.Clients.Http;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Authentication.OpenIdConnect;

/// <summary>
///	Keycloak users client.
/// </summary>
/// <param name="logger">Logger for current instance.</param>
/// <param name="httpClient">Http client for requesting data.</param>
/// <param name="httpResponseParser">Response parser service.</param>
/// <param name="urlProvider"></param>
/// <param name="connectionOptions"></param>
internal sealed class KeycloakUsersService(
	ILogger<KeycloakUsersService> logger,
	HttpClient httpClient,
	IHttpResponseParser httpResponseParser,
	IOpenIdConnectConfigurationProvider urlProvider,
	IOptions<OpenIdConnectServiceConnectionOptions> connectionOptions)
	: HttpServiceClient(logger, httpClient, httpResponseParser), IKeycloakUsersServiceClient
{
	/// <inheritdoc/>
	public async ValueTask<OperationContext<UserInfoResponseModel?>> GetUserInfoAsync(
		string autheticationData,
		CancellationToken cancellationToken = default)
	{
		OpenIdConnectConfiguration requestUrl = await urlProvider.GetConfigurationAsync(connectionOptions.Value.InternalEndpoint, connectionOptions.Value.Realm, cancellationToken)
			.ConfigureAwait(false);
		using var message = new HttpRequestMessage(HttpMethod.Get, requestUrl.UserInfoEndpoint);
		// Preset impersonated token
		_ = message.Headers.TryAddWithoutValidation(HeaderNames.Authorization, autheticationData);

		return await ExecuteHttpRequestAsync<UserInfoResponseModel>(message, cancellationToken)
			.ConfigureAwait(false);
	}
}
