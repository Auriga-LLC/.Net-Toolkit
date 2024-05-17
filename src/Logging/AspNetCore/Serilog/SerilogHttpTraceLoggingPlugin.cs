using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.Logging;

/// <summary>
/// ASP.NET request logging setup plugin.
/// </summary>
internal sealed class SerilogHttpTraceLoggingPlugin :
	FeaturePlugin,
	IConfigurationCheckerPlugin,
	IPipelineConfiguratorPlugin
{
	/// <inheritdoc/>
	public override string Name => "HTTP: Request trace logging";

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.StartupCritical;

	/// <inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		HttpTraceOptions? featureConfiguration = configuration.GetConfigurationModel<HttpTraceOptions>(HttpTraceOptions.SectionName);
		Enabled = featureConfiguration is { TraceHttp: true };

		return configuration;
	}

	/// <inheritdoc/>
	public IApplicationBuilder ConfigurePipeline(IApplicationBuilder app, IHostEnvironment env)
	{
		return !Enabled
			? app
			: app.UseSerilogRequestLogging(opts => opts.IncludeQueryInRequestPath = true);
	}
}
