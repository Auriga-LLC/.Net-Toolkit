using System.Collections.Concurrent;
using System.Globalization;
using System.Text;

namespace Toolkit.Extensions.Authentication.OpenIdConnect;

/// <summary>
/// Keycloak-specific ".well-known" metadata url provider.
/// </summary>
internal sealed class KeycloakWellKnownMetadataUrlProvider : IWellKnownMetadataUrlProvider
{
	private static readonly CompositeFormat s_wellKnownMetadataUrlTemplate = CompositeFormat.Parse("{0}realms/{1}/.well-known/openid-configuration");

	private static readonly ConcurrentDictionary<string, string> s_cachedUrls = new();

	/// <inheritdoc/>
	public string GetWellKnownMetadataUrl(Uri endpoint, string realm)
		=> s_cachedUrls.GetOrAdd(
			realm,
			_ => string.Format(
				CultureInfo.InvariantCulture,
				s_wellKnownMetadataUrlTemplate,
				endpoint.AbsoluteUri.EndsWith('/') ? endpoint.AbsoluteUri : $"{endpoint.AbsoluteUri}/",
				realm));
}
