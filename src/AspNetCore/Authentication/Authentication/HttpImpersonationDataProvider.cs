using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Auriga.Toolkit.Clients.Http;
using Auriga.Toolkit.Logging;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Http context based impersonation authentication data provider.
/// </summary>
/// <param name="logger"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="authTokenProvider"></param>
internal sealed class HttpImpersonationDataProvider(
	ILogger<HttpImpersonationDataProvider> logger,
	IHttpContextAccessor httpContextAccessor,
	IUserAuthenticationTokenProvider authTokenProvider) : IImpersonationDataProvider
{
	/// <inheritdoc/>
	public string GetImpersonationData()
	{
		if (httpContextAccessor.HttpContext == null)
		{
			logger.LogHttpContextNotFound();
			throw new InvalidOperationException(AspNetCoreLogMessages.HttpContextNotFound);
		}

		if (!httpContextAccessor.HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization))
		{
			logger.LogNoAuthHeader();
			throw new AuthenticationFailureException(AuthTokenProviderLog.Messages.NoAuthHeader);
		}

		(string? _, string? authToken) = authTokenProvider.GetAuthenticationToken(httpContextAccessor.HttpContext.Request);
		if (string.IsNullOrWhiteSpace(authToken))
		{
			throw new AuthenticationFailureException(AuthTokenProviderLog.Messages.NoAuthHeader);
		}

		logger.LogAuthDataMalformed();
		return httpContextAccessor.HttpContext.Request.Headers.Authorization.FirstOrDefault()!;
	}
}

internal static partial class AuthTokenProviderLog
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Debug,
		Message = Messages.AuthHeaderSet)]
	public static partial void LogAuthHeaderSet(this ILogger logger, ClientAuthenticationType type);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Debug,
		Message = Messages.TryToExecuteMode)]
	public static partial void LogTryToExecuteMode(this ILogger logger, string method, ClientAuthenticationType type);

	[LoggerMessage(
		EventId = 3,
		Level = LogLevel.Warning,
		Message = Messages.NoAuthHeader)]
	public static partial void LogNoAuthHeader(this ILogger logger);

	[LoggerMessage(
		EventId = 4,
		Level = LogLevel.Warning,
		Message = Messages.AuthDataMalformed)]
	public static partial void LogAuthDataMalformed(this ILogger logger);

	internal static class Messages
	{
		internal const string AuthHeaderSet = "Set auth header using {type} mode";
		internal const string TryToExecuteMode = "We are trying to {method} using {type} type";
		internal const string NoAuthHeader = "There is no requester auth header in HttpContext";
		internal const string AuthDataMalformed = "Auth data malformed in HttpContext, using auth header directly";
	}
}
