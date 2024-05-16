using System.Reflection;
using Auriga.Toolkit.Configuration.Abstractions;
using Auriga.Toolkit.Runtime.Extensions;
using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Bootstrap logger helper class.
/// </summary>
/// <remarks>Finds and creates logger service before application has started.</remarks>
public static class BootstrapLogger
{
	private static Func<string, ILogger>? LoggerFactoryFunc { get; set; }

	/// <summary>
	/// Gets logger service instance.
	/// </summary>
	/// <param name="categoryName">Requested category/class name.</param>
	/// <returns>The <see cref="ILogger"/> for log writing.</returns>
	public static ILogger GetLoggerInstance(string categoryName)
	{
		if (LoggerFactoryFunc != null)
		{
			return LoggerFactoryFunc(categoryName);
		}

		foreach (string assemblyPath in PluginLoader.GetPluginsFileList(PluginLoader.DefaultPluginFileMask, AppContext.BaseDirectory))
		{
			try
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

				LoggerFactoryFunc = instance.GetBootstrapLogger(ConfigurationHelper.BootstrapConfiguration());
				break;
			}
			catch (ReflectionTypeLoadException)
			{
				throw;
			}
		}

		return LoggerFactoryFunc!(categoryName);
	}
}
