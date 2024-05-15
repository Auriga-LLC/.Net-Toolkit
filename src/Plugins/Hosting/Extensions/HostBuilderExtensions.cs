using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Plugins execution methods for <see cref="IHostBuilder"/> extensions.
/// </summary>
public static class HostBuilderExtensions
{
	private const string OperationName = nameof(IPrepareOperationExecutorPlugin.ExecutePrepareOperation);

	/// <inheritdoc cref="IPrepareOperationExecutorPlugin.ExecutePrepareOperation(IHostBuilder, IConfiguration, ILoggingBuilder)"/>
	public static IHostBuilder ExecutePluginsPrepareOperations(
		this IHostBuilder hostBuilder,
		IConfiguration configuration,
		ILoggingBuilder loggingBuilder)
	{
		foreach (IPrepareOperationExecutorPlugin? plugin in PluginLoader.GetPluginsList<IPrepareOperationExecutorPlugin>())
		{
			_ = plugin.ExecutePrepareOperation(hostBuilder, configuration, loggingBuilder);
			if (!plugin.Enabled)
			{
				plugin.Logger.LogFeatureDisabledWithOrderAtOperation(plugin.LoadOrder, plugin.Name, OperationName);
			}
		}
		return hostBuilder;
	}
}
