using Microsoft.AspNetCore.Http;
using Auriga.Toolkit.Http;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// User refresh token provider.
/// </summary>
internal sealed class UserRefreshTokenProvider : IUserRefreshTokenProvider
{
	/// <inheritdoc/>
	public string? GetRefreshToken(HttpRequest request)
	{
		_ = request.Cookies.TryGetValue(CookieName.RefreshToken, out string? refreshToken);
		return refreshToken;
	}
}
