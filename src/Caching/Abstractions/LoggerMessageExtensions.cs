using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Caching;

#pragma warning disable CS1591

public static partial class LoggerMessageExtensions
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Debug,
		Message = CachingLogMessages.CancelWithKey)]
	public static partial void LogCancelledWithKey(this ILogger logger, string methodName, int storageNumber, string key);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Debug,
		Message = CachingLogMessages.CancelWithKeys)]
	public static partial void LogCancelledWithKeys(this ILogger logger, string methodName, int storageNumber, IEnumerable<string> keys);

	[LoggerMessage(
		EventId = 3,
		Level = LogLevel.Debug,
		Message = CachingLogMessages.CancelWithKeyValue)]
	public static partial void LogCancelledWithKeyValue(this ILogger logger, string methodName, int storageNumber, string key, string value);

	[LoggerMessage(
		EventId = 4,
		Level = LogLevel.Debug,
		Message = CachingLogMessages.CancelWithKeyValues)]
	public static partial void LogCancelledWithKeyvalues(
		this ILogger logger,
		string methodName,
		int storageNumber,
		string key,
		IEnumerable<string> values);

	[LoggerMessage(
		EventId = 5,
		Level = LogLevel.Trace,
		Message = CachingLogMessages.CalledKey)]
	public static partial void LogCalledWithKey(this ILogger logger, string methodName, int storageNumber, string key);

	[LoggerMessage(
		EventId = 6,
		Level = LogLevel.Trace,
		Message = CachingLogMessages.CalledKeys)]
	public static partial void LogCalledWithKeys(this ILogger logger, string methodName, int storageNumber, IEnumerable<string> keys);

	[LoggerMessage(
		EventId = 7,
		Level = LogLevel.Trace,
		Message = CachingLogMessages.CalledKeyValue)]
	public static partial void LogCalledWithKeyValue(this ILogger logger, string methodName, int storageNumber, string key, string value);

	[LoggerMessage(
		EventId = 8,
		Level = LogLevel.Trace,
		Message = CachingLogMessages.CalledKeyValues)]
	public static partial void LogCalledWithKeyValues(
		this ILogger logger,
		string methodName,
		int storageNumber,
		string key,
		IEnumerable<string> values);

	[LoggerMessage(
		EventId = 9,
		Level = LogLevel.Debug,
		Message = CachingLogMessages.SavingValuesToStorage)]
	public static partial void LogSavingValuesToStorage(this ILogger logger, object values, int storageNumber);

	[LoggerMessage(
		EventId = 10,
		Level = LogLevel.Debug,
		Message = CachingLogMessages.DeletingKeyFromStorage)]
	public static partial void LogDeletingKeyFromStorage(this ILogger logger, string key, int storageNumber);

	[LoggerMessage(
		EventId = 11,
		Level = LogLevel.Debug,
		Message = CachingLogMessages.DeletingKeysFromStorage)]
	public static partial void LogDeletingKeysFromStorage(this ILogger logger, IEnumerable<string> keys, int storageNumber);
}

#pragma warning restore CS1591
