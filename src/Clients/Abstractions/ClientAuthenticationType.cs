namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// "Log in" mode type for remote API requests.
/// </summary>
public enum ClientAuthenticationType
{
	/// <summary>
	/// Requests will use plain user/password pair.
	/// </summary>
	UserPassword = 0,

	/// <summary>
	/// Requests will use certificate.
	/// </summary>
	Certificate = 1,

	/// <summary>
	/// Requests will use token.
	/// </summary>
	Token = 2,

	/// <summary>
	/// Requests will be sent without authentication data.
	/// </summary>
	Anonymous = 3
}
