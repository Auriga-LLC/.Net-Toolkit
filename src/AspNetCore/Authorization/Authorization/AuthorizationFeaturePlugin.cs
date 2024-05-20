using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.AspNetCore.Authorization;

/// <summary>
/// Http authorization feature setup plugin.
/// </summary>
internal sealed class AuthorizationFeaturePlugin :
	FeaturePlugin,
	IConfigurationCheckerPlugin,
	IServiceConfiguratorPlugin,
	IPipelineConfiguratorPlugin
{
	private AuthorizationFeatureOptions? _configuration;

	/// <inheritdoc/>
	public override string Name => AuthorizationPluginConstants.FeatureName;

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.HttpAuthorization;

	/// <inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		_configuration = configuration.GetConfiguration<AuthorizationFeatureOptions>(AuthorizationFeatureOptions.SectionName);
		Enabled = _configuration?.Enabled == true;
		return configuration;
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		if (!Enabled)
		{
			return services;
		}

		return services
			.ConfigureOptions<AuthorizationFeatureOptions>(configuration, AuthorizationFeatureOptions.SectionName)
			.ConfigureAuthorizationServices(configuration);
	}

	/// <inheritdoc/>
	public IApplicationBuilder ConfigurePipeline(IApplicationBuilder app, IHostEnvironment env)
	{
		ArgumentNullException.ThrowIfNull(app);

		return Enabled ? app.UseAuthorization() : app;
	}
}
