using Microsoft.AspNetCore.Http;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// User refresh token provider contract.
/// </summary>
public interface IUserRefreshTokenProvider
{
	/// <summary>
	/// Get refresh token from HTTP-only cookies.
	/// </summary>
	/// <param name="request">Current HttpRequest.</param>
	/// <returns>Refresh token.</returns>
	string? GetRefreshToken(HttpRequest request);
}
