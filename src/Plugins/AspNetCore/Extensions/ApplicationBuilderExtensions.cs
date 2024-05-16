using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Plugins execution methods accessor extensions.
/// </summary>
public static class ApplicationBuilderExtensions
{
	private const string OperationName = nameof(IPipelineConfiguratorPlugin.ConfigurePipeline);

	///<inheritdoc cref="IPipelineConfiguratorPlugin.ConfigurePipeline(IApplicationBuilder, IHostEnvironment)"/>
	public static IApplicationBuilder ExecutePluginsConfigurePipeline(this IApplicationBuilder app)
	{
		ArgumentNullException.ThrowIfNull(app);

		foreach (IPipelineConfiguratorPlugin? plugin in PluginLoader.GetPluginsList<IPipelineConfiguratorPlugin>())
		{
			_ = plugin.ConfigurePipeline(app, app.ApplicationServices.GetRequiredService<IHostEnvironment>());
			if (!plugin.Enabled)
			{
				plugin.Logger.LogFeatureDisabledWithOrderAtOperation(plugin.LoadOrder, plugin.Name, OperationName);
			}
		}

		return app;
	}
}
