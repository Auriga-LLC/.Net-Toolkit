using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Log messages.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ConfigurationLogMessages
{
#pragma warning disable CS1591
	public const string SettingsSetupNotFound = "Requested configuration \"{section}\" not found";
	public const string SettingsSetupNotFoundNotRegistered = $"{SettingsSetupNotFound} and will not be registered";
	public const string SettingsMissingValue = "Configuration section \"{section}\" missing value(s) for \"({key}\"";
	public const string PreventedMultipleRegistration = "Prevented multiple IOptions<{type}> registration against section:\"{configSection}\"";
#pragma warning restore CS1591
}
