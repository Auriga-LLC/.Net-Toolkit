using Microsoft.Extensions.Hosting;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Plugins execution methods for <see cref="IHost"/> extensions.
/// </summary>
public static class HostExtensions
{
	private const string OperationName = nameof(IPostOperationExecutorPlugin.ExecutePostOperation);

	/// <inheritdoc cref="IPostOperationExecutorPlugin.ExecutePostOperation(IHost)"/>
	public static IHost ExecutePluginsPostOperations(this IHost app)
	{
		return app.ExecutePluginsPostOperations<IPostOperationExecutorPlugin>();
	}

	/// <inheritdoc cref="IPostOperationExecutorPlugin.ExecutePostOperation(IHost)"/>
	public static IHost ExecutePluginsPostOperations<T>(this IHost app)
		where T : class, IPostOperationExecutorPlugin
	{
		foreach (T? plugin in PluginLoader.GetPluginsList<T>())
		{
			_ = plugin.ExecutePostOperation(app);
			if (!plugin.Enabled)
			{
				plugin.Logger.LogFeatureDisabledWithOrderAtOperation(plugin.LoadOrder, plugin.Name, OperationName);
			}
		}

		foreach (IFeaturePlugin? plugin in PluginLoader.Plugins
			.Where(x => x.Enabled)
			.OrderBy(o => o.LoadOrder)
			.DistinctBy(k => k.Name))
		{
			plugin.Logger.LogFeatureEnabledWithOrder(plugin.LoadOrder, plugin.Name, plugin.Enabled);
		}

		PluginLoader.Cleanup();
		return app;
	}
}
