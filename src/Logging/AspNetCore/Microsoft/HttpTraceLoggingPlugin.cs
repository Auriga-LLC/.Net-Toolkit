using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.Http;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.Logging;

/// <summary>
/// ASP.NET request logging setup plugin.
/// </summary>
internal sealed class HttpTraceLoggingPlugin :
	FeaturePlugin,
	IConfigurationCheckerPlugin,
	IServiceConfiguratorPlugin,
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

		HttpTraceOptions? featureConfiguration = configuration.GetConfiguration<HttpTraceOptions>(HttpTraceOptions.SectionName);
		Enabled = featureConfiguration?.TraceHttp == true;

		return configuration;
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		return !Enabled
			? services
			: services.AddHttpLogging(logging =>
			{
				logging.LoggingFields =
					HttpLoggingFields.RequestBody
					| HttpLoggingFields.RequestMethod
					| HttpLoggingFields.RequestPath
					| HttpLoggingFields.ResponseBody
					| HttpLoggingFields.ResponseStatusCode;
				_ = logging.RequestHeaders.Add(HeaderName.RequestExecutor);
				_ = logging.RequestHeaders.Add(HeaderName.HandledByOrchestrator);
				_ = logging.RequestHeaders.Add(HeaderName.RequestId);

				logging.MediaTypeOptions.AddText(MediaTypeNames.Application.Json);
				logging.MediaTypeOptions.AddText(MediaTypeNames.Multipart.FormData);

				logging.CombineLogs = true;
			});
	}

	/// <inheritdoc/>
	public IApplicationBuilder ConfigurePipeline(IApplicationBuilder app, IHostEnvironment env)
	{
		return !Enabled
			? app
			: app.UseHttpLogging();
	}
}
