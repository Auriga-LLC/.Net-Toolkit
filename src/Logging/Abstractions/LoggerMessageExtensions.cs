using Microsoft.Extensions.Logging;

namespace Toolkit.Extensions.Logging.Abstractions;

#pragma warning disable CS1591

public static partial class LoggerMessageExtensions
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Trace,
		Message = LogMessages.MethodCalled)]
	public static partial void LogMethodCalled(this ILogger logger, string methodName);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Trace,
		Message = LogMessages.MethodCalled + " with arg: {arg}")]
	public static partial void LogMethodCalledWithArgument(this ILogger logger, string methodName, string? arg);

	[LoggerMessage(
		EventId = 3,
		Level = LogLevel.Trace,
		Message = LogMessages.MethodCalled + " with args: {args}")]
	public static partial void LogMethodCalledWithArguments(this ILogger logger, string methodName, IEnumerable<string?>? args);

	[LoggerMessage(
		EventId = 4,
		Level = LogLevel.Trace,
		Message = LogMessages.MethodCancelled)]
	public static partial void LogMethodCancelled(this ILogger logger, string methodName);

	[LoggerMessage(
		EventId = 5,
		Level = LogLevel.Trace,
		Message = LogMessages.MethodCompleted)]
	public static partial void LogMethodCompleted(this ILogger logger, string methodName);

	[LoggerMessage(
		EventId = 6,
		Level = LogLevel.Error,
		Message = LogMessages.MethodFailed)]
	public static partial void LogMethodFailed(this ILogger logger, string methodName, Exception exception);

	[LoggerMessage(
		EventId = 7,
		Level = LogLevel.Error,
		Message = LogMessages.MethodFailedWithErrors)]
	public static partial void LogMethodFailedWithError(this ILogger logger, string methodName, string? errors);

	[LoggerMessage(
		EventId = 8,
		Level = LogLevel.Error,
		Message = LogMessages.MethodFailedWithErrors)]
	public static partial void LogMethodFailedWithErrors(this ILogger logger, string methodName,
		IEnumerable<string>? errors);

	[LoggerMessage(
		EventId = 101,
		Level = LogLevel.Trace,
		Message = LogMessages.AttributeApplied)]
	public static partial void LogAttributeApplied(this ILogger logger, string attributeName, string memberName);
}

#pragma warning restore CS1591
