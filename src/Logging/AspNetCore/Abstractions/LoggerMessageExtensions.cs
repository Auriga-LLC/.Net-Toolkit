using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Logging;

#pragma warning disable CS1591

public static partial class LoggerMessageExtensions
{
	[LoggerMessage(
		EventId = 90,
		Level = LogLevel.Error,
		Message = AspNetCoreLogMessages.HttpContextNotFound)]
	public static partial void LogHttpContextNotFound(this ILogger logger);

	[LoggerMessage(
		EventId = 91,
		Level = LogLevel.Error,
		Message = AspNetCoreLogMessages.HttpContextLost)]
	public static partial void LogHttpContextLost(this ILogger logger);
}

#pragma warning restore CS1591
