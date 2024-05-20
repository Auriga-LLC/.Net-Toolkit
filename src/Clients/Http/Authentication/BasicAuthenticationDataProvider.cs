using System.Net.Http.Headers;
using Auriga.Toolkit.Authentication.Abstractions;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Clients.Http;

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
