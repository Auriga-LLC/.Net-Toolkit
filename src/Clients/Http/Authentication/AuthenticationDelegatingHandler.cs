using System.Net;
using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// HttpMessage Authentication handler for remote service clients.
/// </summary>
/// <param name="logger">Logger service.</param>
/// <param name="configuration">LogIn policy configuration.</param>
/// <param name="authenticationHeaderProvider">Authentication header provider client.</param>
internal sealed class AuthenticationDelegatingHandler(
	ILogger<AuthenticationDelegatingHandler> logger,
	ClientAuthenticationOptions configuration,
	IHttpServiceClientAuthenticationDataProvider authenticationHeaderProvider) : DelegatingHandler
{
	/// <inheritdoc/>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		if (authenticationHeaderProvider == null)
		{
			throw new InvalidOperationException("LoginClient is null");
		}

		if (request.Headers.Authorization != null)
		{
			logger.LogFailedToSetExistingRequestHeader(nameof(request.Headers.Authorization));
			return await base.SendAsync(request, cancellationToken)
				.ConfigureAwait(false);
		}

		request.Headers.Authorization = await authenticationHeaderProvider.GetAuthenticationDataAsync(configuration, cancellationToken)
			.ConfigureAwait(false);

		HttpResponseMessage response = await base.SendAsync(request, cancellationToken)
			.ConfigureAwait(false);

		// Perhaps token been expired while we was sending request
		if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
		{
			request.Headers.Authorization = await authenticationHeaderProvider.GetAuthenticationDataAsync(configuration, cancellationToken)
				.ConfigureAwait(false);
			response = await base.SendAsync(request, cancellationToken)
				.ConfigureAwait(false);
		}

		return response;
	}
}
