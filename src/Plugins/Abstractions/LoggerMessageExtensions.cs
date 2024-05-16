using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Plugins;

#pragma warning disable CS1591

public static partial class LoggerMessageExtensions
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Trace,
		Message = LogMessages.LookingForPlugins)]
	public static partial void LogLookingForPlugins(this ILogger logger, string directory, string type, string mask);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Trace,
		Message = LogMessages.FoundAssemblies)]
	public static partial void LogFoundAssemblies(this ILogger logger, int filesCount);

	[LoggerMessage(
		EventId = 3,
		Level = LogLevel.Trace,
		Message = LogMessages.ReusedPluginInstance)]
	public static partial void LogReusedPluginInstance(this ILogger logger, int order, string pluginName, string pluginContract);

	[LoggerMessage(
		EventId = 4,
		Level = LogLevel.Warning,
		Message = LogMessages.PluginCantBeActivated)]
	public static partial void LogPluginCantBeActivated(this ILogger logger, string typeName);

	[LoggerMessage(
		EventId = 5,
		Level = LogLevel.Trace,
		Message = LogMessages.FeatureSetupFoundPlugin)]
	public static partial void LogFeatureSetupFoundPlugin(this ILogger logger, string pluginName, string assemblyName);

	[LoggerMessage(
		EventId = 6,
		Level = LogLevel.Warning,
		Message = LogMessages.PluginActivationFailed)]
	public static partial void LogPluginActivationFailed(this ILogger logger, string typeName, Exception error);

	[LoggerMessage(
		EventId = 7,
		Level = LogLevel.Warning,
		Message = LogMessages.AssemblyLoadingFailed)]
	public static partial void LogAssemblyLoadingFailed(this ILogger logger, string pluginName, Exception error);

	[LoggerMessage(
		EventId = 8,
		Level = LogLevel.Warning,
		Message = LogMessages.PluginCachingFailed)]
	public static partial void LogPluginCachingFailed(this ILogger logger, string pluginFileMask);

	[LoggerMessage(
		EventId = 9,
		Level = LogLevel.Information,
		Message = LogMessages.FeatureEnabledWithOrder)]
	public static partial void LogFeatureEnabledWithOrder(this ILogger logger, int order, string featureName, bool isEnabled);

	[LoggerMessage(
		EventId = 10,
		Level = LogLevel.Information,
		Message = LogMessages.FeatureDisabledWithOrderAtOperation)]
	public static partial void LogFeatureDisabledWithOrderAtOperation(this ILogger logger, int order, string featureName, string operationName);

	[LoggerMessage(
		EventId = 11,
		Level = LogLevel.Error,
		Message = LogMessages.FeatureSetupDeprecatedConfiguration)]
	public static partial void LogFeatureSetupDeprecatedConfiguration(this ILogger logger, string section, string manual);

	[LoggerMessage(
		EventId = 12,
		Level = LogLevel.Error,
		Message = LogMessages.FeatureSetupFailedSeeManual)]
	public static partial void LogFeatureSetupFailedSeeManual(this ILogger logger, string feature, string relatedFeature, string manual);
}

#pragma warning restore CS1591
