using Microsoft.Extensions.Configuration;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Plugins execution methods for <see cref="IConfiguration"/> extensions.
/// </summary>
public static class ConfigurationExtensions
{
	private const string OperationName = nameof(IConfigurationCheckerPlugin.CheckConfiguration);

	/// <inheritdoc cref="IConfigurationCheckerPlugin.CheckConfiguration(IConfiguration)"/>
	public static IConfiguration ExecutePluginsCheckConfiguration(this IConfiguration configuration)
	{
		foreach (IConfigurationCheckerPlugin? plugin in PluginLoader.GetPluginsList<IConfigurationCheckerPlugin>())
		{
			_ = plugin.CheckConfiguration(configuration);
			if (!plugin.Enabled)
			{
				plugin.Logger.LogFeatureDisabledWithOrderAtOperation(plugin.LoadOrder, plugin.Name, OperationName);
			}
		}

		return configuration;
	}
}
