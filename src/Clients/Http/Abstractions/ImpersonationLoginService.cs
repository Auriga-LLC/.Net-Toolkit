using System.Net.Http.Headers;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// LogIn service for use with user impersonation mode.
/// </summary>
/// <remarks>Uses current user token.</remarks>
/// <param name="impersonationDataProvider">User authentication data provider.</param>
public sealed class ImpersonationLoginService(IImpersonationDataProvider impersonationDataProvider) : IHttpServiceClientAuthenticationDataProvider
{
	/// <inheritdoc/>
	public Task<AuthenticationHeaderValue> GetAuthenticationDataAsync(ClientAuthenticationOptions configuration, CancellationToken cancellationToken = default)
		=> Task.FromResult(AuthenticationHeaderValue.Parse(impersonationDataProvider.GetImpersonationData()));
}
