using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Auriga.Toolkit.Runtime.Extensions;

using PluginAssemblyLoader = McMaster.NETCore.Plugins.PluginLoader;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Plugin loader service.
/// </summary>
[ExcludeFromCodeCoverage]
public static class PluginLoader
{
	private const string LibraryFileMask = "*.dll";

	internal const string DefaultPluginFileMask = @".*\.dll";

	private static readonly ILogger s_logger = BootstrapLogger.GetLoggerInstance(typeof(PluginLoader).FullName!);
	private static readonly Dictionary<object, IReadOnlyCollection<Type>> s_pluginsCache = [];

	/// <summary>
	/// Loaded plugins list.
	/// </summary>
	public static IReadOnlyCollection<IFeaturePlugin> Plugins => s_pluginsCache.Keys.Select(k => (IFeaturePlugin)k).ToList();

	/// <summary>
	/// Initializes a new instance of the <see cref="PluginLoader"/> class.
	/// </summary>
	/// <remarks>Additionaly builds up plugins cache.</remarks>
	static PluginLoader()
	{
		s_logger.LogLookingForPlugins(AppContext.BaseDirectory, typeof(IFeaturePlugin).FullName!, DefaultPluginFileMask);

		HashSet<string> assemblyPaths = GetPluginsFileList(DefaultPluginFileMask, AppContext.BaseDirectory);
		s_logger.LogFoundAssemblies(assemblyPaths.Count);

		foreach (string assemblyPath in assemblyPaths)
		{
			try
			{
				foreach (Type pluginType in LoadAssembly(assemblyPath).GetImplementationTypes<IFeaturePlugin>())
				{
					InitializePlugin(assemblyPath, pluginType);
				}
			}
			catch (ReflectionTypeLoadException ex)
			{
				s_logger.LogAssemblyLoadingFailed(assemblyPath, ex);
			}
		}
	}

	/// <summary>
	/// Gets list of plugins of specific type.
	/// </summary>
	/// <typeparam name="T">Requested plugin type.</typeparam>
	/// <returns>Plugins list of specific type.</returns>
	public static IEnumerable<T> GetPluginsList<T>() where T : class, IFeaturePlugin
	{
		foreach (T? plugin in s_pluginsCache
			.Where(cachedPlugin => cachedPlugin.Value.Contains(typeof(T)))
			.Select(cache => (T)cache.Key)
			.Where(e => e.Enabled)
			.OrderBy(o => o.LoadOrder))
		{
			s_logger.LogReusedPluginInstance(plugin.LoadOrder, plugin.Name, typeof(T).Name);
			yield return plugin;
		}
	}

	/// <summary>
	/// Cleanup plugins cache.
	/// </summary>
	public static void Cleanup()
		=> s_pluginsCache.Clear();

	internal static Assembly LoadAssembly(string assemblyPath)
	{
		using var pluginLoader = PluginAssemblyLoader.CreateFromAssemblyFile(assemblyPath, config =>
		{
			config.PreferSharedTypes = true;
			config.LoadInMemory = false;
		}); // This ensures that the plugin resolves to the same version of DependencyInjection and ASP.NET Core that the current app uses
		return Array.Find(AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ToArray(), x => x.Location == assemblyPath)
			?? pluginLoader.LoadDefaultAssembly();
	}

	private static void InitializePlugin(string assemblyPath, Type pluginType)
	{
		var implementedPlugins = pluginType.GetInterfaces()
			.Where(t => t.IsAssignableTo(typeof(IFeaturePlugin)) && t != typeof(IFeaturePlugin))
			.ToList();

		try
		{
			if (Activator.CreateInstanceFrom(assemblyPath, pluginType.FullName!)?.Unwrap() is not IFeaturePlugin pluginInstance)
			{
				s_logger.LogPluginCantBeActivated(pluginType.Name);
				return;
			}

			pluginInstance.Logger = BootstrapLogger.GetLoggerInstance(pluginInstance.GetType().FullName!);
			s_logger.LogFeatureSetupFoundPlugin(pluginInstance.Name, Path.GetFileNameWithoutExtension(assemblyPath));

			if (!s_pluginsCache.TryAdd(pluginInstance, implementedPlugins))
			{
				s_logger.LogPluginCachingFailed(pluginInstance.GetType().FullName!);
			}
		}
		catch (Exception ex)
		{
			s_logger.LogPluginActivationFailed(pluginType.Name, ex);
			throw;
		}
	}

	internal static HashSet<string> GetPluginsFileList(string pluginFileMask, string filesPath) =>
		Directory.GetFiles(filesPath, LibraryFileMask)
			.Where(path => new Regex(pluginFileMask).IsMatch(Path.GetFileName(path)))
			.ToHashSet();
}
