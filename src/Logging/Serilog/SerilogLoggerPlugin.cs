using Auriga.Toolkit.Plugins;
using Auriga.Toolkit.Runtime.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;

namespace Auriga.Toolkit.Logging.Serilog;

/// <summary>
/// ASP.NET logging provider setup plugin.
/// </summary>
internal sealed class SerilogLoggerPlugin :
	FeaturePlugin,
	IPrepareOperationExecutorPlugin
{
	/// <inheritdoc/>
	public override string Name => "Serilog logging provider";

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.StartupCritical;

	/// <inheritdoc/>
	public IHostBuilder ExecutePrepareOperation(IHostBuilder hostBuilder, IConfiguration configuration, ILoggingBuilder loggingBuilder)
	{
		Enabled = configuration.GetSection(nameof(Serilog)).Exists() && !EnvironmentHelper.IsAspNetCoreApp();

		if (!Enabled)
		{
			return hostBuilder;
		}

		// Remove other log providers
		_ = loggingBuilder.ClearProviders();

		// Init logger config

		// Setup Serilog
		_ = hostBuilder.UseSerilog(
			new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.WithExceptionDetails()
				.CreateLogger());

		return hostBuilder;
	}
}
