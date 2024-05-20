using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Configuration;

#pragma warning disable CS1591

public static partial class LoggerMessageExtensions
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Error,
		Message = ConfigurationLogMessages.SettingsSetupNotFound)]
	public static partial void LogSettingsNotFound(this ILogger logger, string section);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Error,
		Message = ConfigurationLogMessages.SettingsMissingValue)]
	public static partial void LogSettingsMissingValue(this ILogger logger, string section, string key);
}

#pragma warning restore CS1591
