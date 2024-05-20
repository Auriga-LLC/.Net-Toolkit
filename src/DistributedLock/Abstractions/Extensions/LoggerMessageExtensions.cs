using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.DistributedLock.Abstractions.Extensions;

#pragma warning disable CS1591

public static partial class LoggerMessageExtensions
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Warning,
		Message = DistributedLockLogMessages.LockNotAcquired)]
	public static partial void LogLockNotAcquired(this ILogger logger, string lockName);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Debug,
		Message = DistributedLockLogMessages.LockAcquired)]
	public static partial void LogLockAcquired(this ILogger logger, string lockName);

	[LoggerMessage(
		EventId = 3,
		Level = LogLevel.Trace,
		Message = DistributedLockLogMessages.LockRequested)]
	public static partial void LogLockRequested(
		this ILogger logger,
		string mode,
		string lockKey,
		TimeSpan expiryTime,
		TimeSpan waitTime,
		TimeSpan retryTime);
}

#pragma warning restore CS1591
