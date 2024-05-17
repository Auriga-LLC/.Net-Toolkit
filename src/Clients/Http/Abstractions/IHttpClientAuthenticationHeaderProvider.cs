using System.Net.Http.Headers;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// "Authentication data" provider for http clients contract.
/// </summary>
public interface IHttpServiceClientAuthenticationDataProvider
{
	/// <summary>
	/// Asynchronously builds authentication header data.
	/// </summary>
	/// <param name="configuration">LogIn policy configuration.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="Task"/> that represents the asynchronous request operation.</returns>
	Task<AuthenticationHeaderValue> GetAuthenticationDataAsync(ClientAuthenticationOptions configuration, CancellationToken cancellationToken = default);
}
