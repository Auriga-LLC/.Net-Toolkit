using System.Net.Http.Headers;
using Toolkit.Extensions.Authentication.Abstractions;
using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// KeyCloak-specific identity provider.
/// </summary>
/// <remarks>Builds authentication header value equal to "user:password" as base64 encoded string.</remarks>
public sealed class BasicAuthenticationDataProvider : IHttpServiceClientAuthenticationDataProvider
{
	/// <inheritdoc/>
	public Task<AuthenticationHeaderValue> GetAuthenticationDataAsync(ClientAuthenticationOptions configuration, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		return Task.FromResult(
			new AuthenticationHeaderValue(
				AuthenticationScheme.Basic.GetDescription(),
				$"{configuration.UserId}:{configuration.UserSecret}".EncodeToBase64()));
	}
}
