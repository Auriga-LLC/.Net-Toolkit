using Microsoft.AspNetCore.Http;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// User authentication token provider contract.
/// </summary>
public interface IUserAuthenticationTokenProvider
{
	/// <summary>
	/// Gets auth token from header and HTTP-only cookies.
	/// </summary>
	/// <param name="request">Current HttpRequest.</param>
	/// <returns>All auth token parts including scheme.</returns>
	(string? scheme, string? authenticationToken) GetAuthenticationToken(HttpRequest request);
}
