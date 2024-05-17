using System.Reflection;
using Microsoft.Extensions.Logging;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.Logging;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Bootstrap logger helper class.
/// </summary>
/// <remarks>Finds and creates logger service before application has started.</remarks>
internal static class BootstrapLogger
{
	private static IBootstrapLoggerPlugin? s_loggerPlugin;

	/// <summary>
	/// Gets logger service instance.
	/// </summary>
	/// <param name="categoryName">Requested category/class name.</param>
	/// <returns>The <see cref="ILogger"/> for log writing.</returns>
	public static ILogger GetLoggerInstance(string categoryName)
	{
		if (s_loggerPlugin != null)
		{
			return s_loggerPlugin.GetBootstrapLogger(categoryName);
		}

		foreach (string assemblyPath in PluginLoader.GetPluginsFileList(PluginLoader.DefaultPluginFileMask, AppContext.BaseDirectory))
		{
			Assembly pluginAssembly = PluginLoader.LoadAssembly(assemblyPath);
			Type? pluginType = pluginAssembly.GetImplementationTypes<IBootstrapLoggerPlugin>().FirstOrDefault();
			if (pluginType == null)
			{
				continue;
			}

			if (Activator.CreateInstanceFrom(assemblyPath, pluginType.FullName!)?.Unwrap() is not IBootstrapLoggerPlugin instance)
			{
				continue;
			}

			s_loggerPlugin = instance;
			s_loggerPlugin.Init(ConfigurationHelper.BootstrapConfiguration());
			break;
		}

		return s_loggerPlugin!.GetBootstrapLogger(categoryName);
	}

	/// <summary>
	/// Cleanup loggers cache.
	/// </summary>
	public static void Cleanup()
		=> s_loggerPlugin!.Dispose();
}
