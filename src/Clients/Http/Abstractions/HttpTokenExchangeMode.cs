namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// "Exchange" mode type for remote API requests.
/// </summary>
public enum HttpTokenExchangeMode
{
	/// <summary>
	/// Requests will exchange user/password pair.
	/// </summary>
	UserPassword = 0,

	/// <summary>
	/// Requests will exchange offline token.
	/// </summary>
	OfflineToken = 1,

	/// <summary>
	/// Requests will use current user credentials from request context.
	/// </summary>
	Impersonation = 2
}
