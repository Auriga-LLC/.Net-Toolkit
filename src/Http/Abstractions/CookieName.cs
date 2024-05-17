using System.Diagnostics.CodeAnalysis;

namespace Toolkit.Extensions.Http;

/// <summary>
/// List of well known cookie names.
/// </summary>
[ExcludeFromCodeCoverage]
public static class CookieName
{
	/// <summary>
	/// Auth token cookie name.
	/// </summary>
	public const string AuthToken = "X-Session";

	/// <summary>
	/// Refresh token cookie name.
	/// </summary>
	public const string RefreshToken = "X-Refresh";

	/// <summary>
	/// Sse User Id cookie name.
	/// </summary>
	public const string SseUserId = "X-Sse-User";
}
