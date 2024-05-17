using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Auriga.Toolkit.Logging;

internal static class LogLevelMapper
{
	internal static LogEventLevel Map(LogLevel logLevel)
		=> logLevel switch
		{
			LogLevel.Trace => LogEventLevel.Verbose,
			LogLevel.Debug => LogEventLevel.Debug,
			LogLevel.Information => LogEventLevel.Information,
			LogLevel.Warning => LogEventLevel.Warning,
			LogLevel.Error => LogEventLevel.Error,
			LogLevel.Critical => LogEventLevel.Fatal,
			_ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null),
		};
}
