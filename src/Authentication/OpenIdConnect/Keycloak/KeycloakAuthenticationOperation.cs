using System.ComponentModel;

namespace Auriga.Toolkit.Authentication.OpenIdConnect;

/// <summary>
/// API operation types and their Urls.
/// </summary>
public enum KeycloakAuthenticationOperation
{
	/// <summary>
	/// Login Url.
	/// </summary>
	/// <remarks>We got some bug here <see href="https://github.com/keycloak/keycloak/issues/16844"></see>, so scope is forced</remarks>
	[Description("{0}?response_type={1}&client_id={2}&redirect_uri={3}&state={4}&scope=openid")]
	GetLoginUrl,

	/// <summary>
	/// Logout Url.
	/// </summary>
	[Description("{0}?redirect_uri={1}&state={2}")]
	GetLogOutUrl
}
