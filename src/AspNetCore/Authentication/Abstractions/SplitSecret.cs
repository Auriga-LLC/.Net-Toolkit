using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Represents splitted users secret.
/// </summary>
/// <param name="TokenScheme">Used token scheme.</param>
/// <param name="AuthenticationToken">Built authentication token body.</param>
/// <param name="RefreshToken">Refresh token body.</param>
public record SplitSecret(string TokenScheme, string AuthenticationToken, string? RefreshToken)
{
	/// <summary>
	/// Binds <see cref="SplitSecret"/> from Http context.
	/// </summary>
	/// <param name="context">Current Http context</param>
	/// <param name="_">Parameter metadata.</param>
	/// <returns>Built <see cref="SplitSecret"/>.</returns>
	public static ValueTask<SplitSecret> BindAsync(HttpContext context, ParameterInfo _)
	{
		ArgumentNullException.ThrowIfNull(context);

		IUserAuthenticationTokenProvider tokenProvider = context.RequestServices.GetService<IUserAuthenticationTokenProvider>()
			?? throw new InvalidOperationException("IUserAuthenticationTokenProvider implementation missing");

		(string? scheme, string? authToken) = tokenProvider.GetAuthenticationToken(context.Request);
		if (string.IsNullOrWhiteSpace(scheme) || string.IsNullOrWhiteSpace(authToken))
		{
			throw new InvalidOperationException("Missing or not well formed authentication token.");
		}

		IUserRefreshTokenProvider? refreshTokenProvider = context.RequestServices.GetService<IUserRefreshTokenProvider>();
		string? refreshToken = refreshTokenProvider?.GetRefreshToken(context.Request);

		return ValueTask.FromResult(new SplitSecret(scheme, authToken, refreshToken));
	}

	/// <summary>
	/// Gets built authentication header in a standart manner.
	/// </summary>
	/// <returns>Concatenated <see cref="TokenScheme"/> and <see cref="AuthenticationToken"/>.</returns>
	public string AuthenticationHeader => $"{TokenScheme} {AuthenticationToken}";
}
