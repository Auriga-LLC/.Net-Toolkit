using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Pipeline configurator plugin contract.
/// </summary>
public interface IPipelineConfiguratorPlugin : IFeaturePlugin
{
	/// <summary>
	/// Setup plugin-specific pipeline in the specified <see cref="IApplicationBuilder"/>.
	/// </summary>
	/// <param name="app">The <see cref="IApplicationBuilder"/> to build up application pipeline.</param>
	/// <param name="env">The <see cref="IHostEnvironment"/>.</param>
	/// <returns>The <see cref="IApplicationBuilder"/> so that additional calls can be chained.</returns>
	IApplicationBuilder ConfigurePipeline(IApplicationBuilder app, IHostEnvironment env);
}
