using System.Collections.Concurrent;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Auriga.Toolkit.Authentication.OpenIdConnect;

/// <summary>
/// Keycloak-specific OpenId configuration provider.
/// </summary>
/// <param name="wellKnownUrlProvider">Well-known url provider.</param>
internal sealed class KeycloakOpenIdConfigurationProvider(IWellKnownMetadataUrlProvider wellKnownUrlProvider) : IOpenIdConnectConfigurationProvider
{
	private const string DefaultRealm = "master";
	private static readonly ConcurrentDictionary<string, OpenIdConnectConfiguration> s_loadedConfiguration = new();

	/// <inheritdoc/>
	public async ValueTask<OpenIdConnectConfiguration> GetConfigurationAsync(Uri? endpoint, string? realmName = null, CancellationToken cancellationToken = default)
	{
		if (endpoint == null)
		{
			throw new InvalidOperationException("Endpoint is not set");
		}

		realmName ??= DefaultRealm;

		return s_loadedConfiguration.GetOrAdd(
			realmName,
			await OpenIdConnectConfigurationRetriever.GetAsync(
					wellKnownUrlProvider.GetWellKnownMetadataUrl(endpoint, realmName),
					new HttpDocumentRetriever { RequireHttps = false },
					cancellationToken)
				.ConfigureAwait(false)
		);
	}
}
