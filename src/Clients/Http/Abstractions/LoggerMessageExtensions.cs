using System.Net;
using Microsoft.Extensions.Logging;

namespace Toolkit.Extensions.Clients.Http;

#pragma warning disable CS1591

public static partial class LoggerMessageExtensions
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Debug,
		Message = LogMessages.AnonymousRequest)]
	public static partial void LogAnonymousRequest(this ILogger logger);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Trace,
		Message = LogMessages.ExecutingRequest)]
	public static partial void LogExecutingRequest(this ILogger logger, HttpMethod requestMethod, Uri? requestUri, Dictionary<string, IEnumerable<string>> requestHeaders, string requestBody);

	[LoggerMessage(
		EventId = 3,
		Level = LogLevel.Warning,
		Message = LogMessages.ErrorResponse)]
	public static partial void LogErrorReadingResponse(this ILogger logger, HttpResponseMessage response);

	[LoggerMessage(
		EventId = 4,
		Level = LogLevel.Trace,
		Message = LogMessages.CorrectResponse)]
	public static partial void LogSuccessfulResponse(this ILogger logger, string response);

	[LoggerMessage(
		EventId = 5,
		Level = LogLevel.Warning,
		Message = LogMessages.FailedToAddRequestHeader)]
	public static partial void LogFailedToAddRequestHeader(this ILogger logger, string headerName, string headerValue);

	[LoggerMessage(
		EventId = 6,
		Level = LogLevel.Warning,
		Message = LogMessages.HeaderIsAlreadySet)]
	public static partial void LogFailedToSetExistingRequestHeader(this ILogger logger, string headerName);

	[LoggerMessage(
		EventId = 7,
		Level = LogLevel.Trace,
		Message = LogMessages.ReceivedResponse)]
	public static partial void LogReceivedResponse(this ILogger logger, HttpStatusCode responseStatusCode, Dictionary<string, IEnumerable<string>> responseHeaders, string responseContent);
}

#pragma warning restore CS1591
