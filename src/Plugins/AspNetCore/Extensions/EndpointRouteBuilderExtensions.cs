using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Plugins execution methods accessor extensions.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
	private const string OperationName = nameof(IRoutingConfiguratorPlugin.ConfigureRouting);

	///<inheritdoc cref="IRoutingConfiguratorPlugin.ConfigureRouting(IEndpointRouteBuilder)"/>
	public static IApplicationBuilder ExecutePluginsConfigureRouting(this WebApplication app)
	{
		ArgumentNullException.ThrowIfNull(app);

		foreach (IRoutingConfiguratorPlugin? plugin in PluginLoader.GetPluginsList<IRoutingConfiguratorPlugin>())
		{
			_ = plugin.ConfigureRouting(app);
			if (!plugin.Enabled)
			{
				plugin.Logger.LogFeatureDisabledWithOrderAtOperation(plugin.LoadOrder, plugin.Name, OperationName);
			}
		}

		return app;
	}
}
