using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Auriga.Toolkit.Http;
using Auriga.Toolkit.Plugins;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Logging;

/// <summary>
/// ASP.NET Serilog logging provider setup plugin.
/// </summary>
internal sealed class SerilogWebLogPlugin :
	FeaturePlugin,
	IPrepareOperationExecutorPlugin,
	IServiceConfiguratorPlugin
{
	/// <inheritdoc/>
	public override string Name => "ASP.NET: Serilog logging provider";

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.StartupCritical;

	/// <inheritdoc/>
	public IHostBuilder ExecutePrepareOperation(IHostBuilder hostBuilder, IConfiguration configuration, ILoggingBuilder loggingBuilder)
	{
		ArgumentNullException.ThrowIfNull(hostBuilder);
		ArgumentNullException.ThrowIfNull(configuration);
		ArgumentNullException.ThrowIfNull(loggingBuilder);

		Enabled = configuration.GetSection(nameof(Serilog)).Exists() && EnvironmentHelper.IsAspNetCoreApp();
		if (!Enabled)
		{
			return hostBuilder;
		}

		// Remove other log providers
		_ = loggingBuilder.ClearProviders();

		// Setup Serilog
		return hostBuilder.UseSerilog(
			new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.WithExceptionDetails()
				.Enrich.WithCorrelationIdHeader(HeaderName.RequestId)
				.CreateLogger());
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
		=> services.AddLogging();
}
