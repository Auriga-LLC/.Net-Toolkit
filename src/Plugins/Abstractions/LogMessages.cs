using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Plugins related log messages.
/// </summary>
[ExcludeFromCodeCoverage]
public static class LogMessages
{
#pragma warning disable CS1591

	public const string LookingForPlugins = "Looking in \"{directory}\" for plugins \"{type}\" by mask \"{mask}\"";
	public const string FoundAssemblies = "Found \"{filesCount}\" plugin assemblies";
	public const string ReusedPluginInstance = "Reused plugin instance: (#{order}) \"{pluginName}\" as \"{pluginContract}\"";
	public const string PluginCantBeActivated = "Plugin \"{typeName}\" cannot be activated";
	public const string FeatureSetupFoundPlugin = "Found plugin: \"{pluginName}\" in assembly: \"{assemblyName}\"";
	public const string PluginActivationFailed = "Plugin \"{typeName}\" bypassed because of error";
	public const string AssemblyLoadingFailed = "Failed to load plugin assembly {pluginName}";
	public const string PluginCachingFailed = "Failed to add \"{pluginFileMask}\" plugins to cache";
	public const string FeatureEnabledWithOrder = "Feature (#{order}) \"{featureName}\" enabled:\"{isEnabled}\"";
	public const string FeatureDisabledWithOrderAtOperation = "Feature (#{order}) \"{featureName}\" is disabled at \"{operationName}\"";
	public const string FeatureSetupDeprecatedConfiguration = "Deprecated \"{section}\" section found. pls update application settings. Please refer manual: {manual}";
	public const string FeatureSetupFailedSeeManual = "You have enabled \"{feature}\" feature, but \"{relatedFeature}\" is not setup correctly. Please refer manual: {manual}";

#pragma warning restore CS1591
}
