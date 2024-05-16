using Auriga.Toolkit.Plugins;
using Microsoft.AspNetCore.Builder;

namespace Auriga.Toolkit.AspNetCore.Extensions;

/// <summary>
/// Web application builder helper methods extensions.
/// </summary>
public static class WebApplicationBuilderExtensions
{
	/// <summary>
	/// Executes plugins-based workflow and builds web application.
	/// </summary>
	/// <param name="builder">Web application builder.</param>
	/// <returns><see cref="WebApplication"/> so that additional calls can be chained.</returns>
	public static WebApplication BuildApplication(this WebApplicationBuilder builder)
	{
		ArgumentNullException.ThrowIfNull(builder);

		_ = builder.Host.ExecutePluginsPrepareOperations(builder.Configuration, builder.Logging);

		_ = builder.Configuration.ExecutePluginsCheckConfiguration();

		_ = builder.Services.ExecutePluginsConfigureServices(builder.Configuration);

		WebApplication app = builder.Build();

		_ = app.ExecutePluginsConfigurePipeline();

		_ = app.ExecutePluginsConfigureRouting();

		_ = app.ExecutePluginsPostOperations();

		return app;
	}
}
