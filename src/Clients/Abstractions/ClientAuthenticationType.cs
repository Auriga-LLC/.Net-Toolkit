namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// "Log in" mode type for remote API requests.
/// </summary>
public enum ClientAuthenticationType
{
	/// <summary>
	/// Requests will use user/password pair.
	/// </summary>
	UserPassword = 0,

	/// <summary>
	/// Requests will use offline token.
	/// </summary>
	OfflineToken = 1,

	/// <summary>
	/// Requests will use current user credentials from Http context.
	/// </summary>
	Impersonation = 2,

	/// <summary>
	/// Requests will be sent without authentication data.
	/// </summary>
	Anonymous = 3
}
