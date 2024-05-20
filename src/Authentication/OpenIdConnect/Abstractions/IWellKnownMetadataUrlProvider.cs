namespace Auriga.Toolkit.Authentication.OpenIdConnect;

/// <summary>
/// Contract for ".well-known" metadata url provider.
/// </summary>
public interface IWellKnownMetadataUrlProvider
{
	/// <summary>
	/// Gets internal metadata address for communication.
	/// </summary>
	/// <value>Returns <c>{InternalAuthority}/.well-known/openid-configuration</c>.</value>
	/// <exception cref="InvalidOperationException">If endpoint is not set.</exception>
	string GetWellKnownMetadataUrl(Uri endpoint, string realm);
}
