using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auriga.Toolkit.Configuration;
using SharedMessages = Auriga.Toolkit.Logging.LogMessages;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.AspNetCore.CORS;

/// <summary>
/// Http CORS feature.
/// </summary>
internal sealed class CorsFeaturePlugin : FeaturePlugin, IConfigurationCheckerPlugin, IServiceConfiguratorPlugin, IPipelineConfiguratorPlugin
{
	private const string AllowAllPolicyName = "AllowAll";
	private string _defaultPolicyName = AllowAllPolicyName;
	private CorsFeatureOptions? _corsFeatureConfiguration;

	/// <inheritdoc/>
	public override string Name => "HTTP: CORS Support";

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.DependsOnHttpServer;

	/// <inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		IConfigurationSection currentSection = configuration.GetSection(CorsFeatureOptions.SectionName);
		if (!currentSection.Exists())
		{
			Enabled = false;
		}

		_corsFeatureConfiguration = configuration.GetConfigurationModel<CorsFeatureOptions>(CorsFeatureOptions.SectionName);
		Enabled = _corsFeatureConfiguration?.Enabled == true;

		_defaultPolicyName = _corsFeatureConfiguration?.Policies?.FirstOrDefault(p => p.Value.IsDefault).Key ?? _defaultPolicyName;

		return configuration;
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		if (!Enabled)
		{
			return services;
		}

		// Register config
		_ = services.RegisterConfigurationIfExists<CorsFeatureOptions>(configuration, CorsFeatureOptions.SectionName);

		return services.AddCors(options =>
		{
			if (_corsFeatureConfiguration?.Policies?.Any() != true)
			{
				options.AddPolicy(AllowAllPolicyName, configurePolicy: policyBuilder =>
				{
					_ = policyBuilder
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowAnyOrigin();
				});

				return;
			}

			foreach ((string policyName, CorsPolicyOptions policyConfig) in _corsFeatureConfiguration.Policies)
			{
				options.AddPolicy(policyName, configurePolicy: policyBuilder =>
					{
						_ = policyConfig.AllowedOrigins?.Count > 0
							? policyBuilder.WithOrigins([..policyConfig.AllowedOrigins])
							: policyBuilder.AllowAnyOrigin();

						_ = policyConfig.AllowedMethods?.Count > 0
							? policyBuilder.WithMethods([..policyConfig.AllowedMethods])
							: policyBuilder.AllowAnyMethod();

						_ = policyConfig.AllowedHeaders?.Count > 0
							? policyBuilder.WithHeaders([..policyConfig.AllowedHeaders])
							: policyBuilder.AllowAnyHeader();

						if (policyConfig.AllowCredentials)
						{
							_ = policyBuilder.AllowCredentials();
						}
					});
			}
		});
	}

	/// <inheritdoc/>
	public IApplicationBuilder ConfigurePipeline(IApplicationBuilder app, IHostEnvironment env)
	{
		ArgumentNullException.ThrowIfNull(app);

		return Enabled ? app.UseCors(_defaultPolicyName) : app;
	}
}
