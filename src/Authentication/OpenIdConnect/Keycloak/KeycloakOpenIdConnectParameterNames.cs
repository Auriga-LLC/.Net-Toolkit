namespace Auriga.Toolkit.Authentication.OpenIdConnect;

/// <summary>
/// Keycloak-specific OpenID connect parameter names.
/// </summary>
public static class KeycloakOpenIdConnectParameterNames
{
	/// <summary>
	/// Token body parameter name.
	/// </summary>
	public const string Token = "token";

	/// <summary>
	/// Token type parameter name.
	/// </summary>
	public const string TokenType = "token_type_hint";
}
