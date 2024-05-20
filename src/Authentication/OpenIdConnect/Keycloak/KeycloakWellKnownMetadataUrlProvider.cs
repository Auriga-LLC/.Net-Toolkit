namespace Auriga.Toolkit.Authentication.OpenIdConnect;

/// <summary>
/// Keycloak-specific ".well-known" metadata url provider.
/// </summary>
internal sealed class KeycloakWellKnownMetadataUrlProvider : IWellKnownMetadataUrlProvider
{
	/// <inheritdoc/>
	public string GetWellKnownMetadataUrl(Uri endpoint, string realm)
		=> DefaultOpenIdConnectWellKnownMetadataUrlProvider.GetWellKnownMetadataUrl(endpoint, realm);
}
