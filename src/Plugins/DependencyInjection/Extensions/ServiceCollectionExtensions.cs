using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Plugins execution methods for <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
	private const string OperationName = nameof(IServiceConfiguratorPlugin.ConfigureServices);

	/// <inheritdoc cref="IServiceConfiguratorPlugin.ConfigureServices(IServiceCollection, IConfiguration)"/>
	public static IServiceCollection ExecutePluginsConfigureServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		return services.ExecutePluginsConfigureServices<IServiceConfiguratorPlugin>(configuration);
	}

	/// <inheritdoc cref="IServiceConfiguratorPlugin.ConfigureServices(IServiceCollection, IConfiguration)"/>
	public static IServiceCollection ExecutePluginsConfigureServices<T>(
		this IServiceCollection services,
		IConfiguration configuration)
		where T : class, IServiceConfiguratorPlugin
	{
		foreach (T? plugin in PluginLoader.GetPluginsList<T>())
		{
			_ = plugin.ConfigureServices(services, configuration);
			if (!plugin.Enabled)
			{
				plugin.Logger.LogFeatureDisabledWithOrderAtOperation(plugin.LoadOrder, plugin.Name, OperationName);
			}
		}

		return services;
	}
}
