using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Auriga.Toolkit.Http;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// User authentication token provider.
/// </summary>
internal sealed class UserAuthenticationTokenProvider : IUserAuthenticationTokenProvider
{
	/// <inheritdoc/>
	public (string? scheme, string? authenticationToken) GetAuthenticationToken(HttpRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);

		if (!request.Cookies.TryGetValue(CookieName.AuthToken, out string? cookiePart) || string.IsNullOrWhiteSpace(cookiePart))
		{
			return (null, null);
		}

		if (!request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues headerPart))
		{
			return (null, null);
		}

		string[] headerParts = headerPart.ToString().Split(' ');
		if (headerParts.Length != 2)
		{
			return (null, null);
		}

		return (scheme: headerParts[0], authenticationToken: cookiePart + "." + headerParts[1]);
	}
}
