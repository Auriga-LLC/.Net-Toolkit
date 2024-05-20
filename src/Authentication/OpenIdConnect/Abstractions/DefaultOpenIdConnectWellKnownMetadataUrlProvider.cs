using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Authentication.OpenIdConnect;

public static class DefaultOpenIdConnectWellKnownMetadataUrlProvider
{
	private static readonly CompositeFormat s_wellKnownMetadataUrlTemplate = CompositeFormat.Parse("{0}realms/{1}/.well-known/openid-configuration");

	private static readonly ConcurrentDictionary<string, string> s_cachedUrls = new();
	
	public static string GetWellKnownMetadataUrl(Uri endpoint, string realm)
		=> s_cachedUrls.GetOrAdd(
			realm,
			_ => string.Format(
				CultureInfo.InvariantCulture,
				s_wellKnownMetadataUrlTemplate,
				endpoint.GetNormalizedAbsoluteUri(),
				realm));
}
