using Auriga.Toolkit.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auriga.Toolkit.AspNetCore.OpenApi;

/// <summary>
/// OpenAPI support feature plugin.
/// </summary>
internal sealed class OpenApiFeaturePlugin : FeaturePlugin, IServiceConfiguratorPlugin, IPipelineConfiguratorPlugin
{
	///<inheritdoc/>
	public override string Name => "OpenAPI + Swagger";

	///<inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.DependsOnHttpServer + 1;

	///<inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
		=> services
			.AddEndpointsApiExplorer()
			.AddSwaggerGen();

	///<inheritdoc/>
	public IApplicationBuilder ConfigurePipeline(IApplicationBuilder app, IHostEnvironment env)
	{
		if (!env.IsDevelopment())
			return app;

		return app
			.UseSwagger()
			.UseSwaggerUI();
	}
}
