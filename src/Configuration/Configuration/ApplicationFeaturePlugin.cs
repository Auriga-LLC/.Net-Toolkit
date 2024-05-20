using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Application name feature plugin.
/// </summary>
internal sealed class ApplicationFeaturePlugin :
	FeaturePlugin,
	IConfigurationCheckerPlugin,
	IServiceConfiguratorPlugin
{
	/// <inheritdoc/>
	public override string Name => ConfigurationPluginConstants.FeatureName;

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.ConfigurationCritical;

	/// <inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		IConfigurationSection configurationSection = configuration.GetSection(ApplicationConfiguration.SectionName);
		if (!configurationSection.Exists())
		{
			Enabled = false;
			Logger.LogSettingsNotFound(ApplicationConfiguration.SectionName);
		}

		return configuration;
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
		=> services
			.ConfigureOptions<ApplicationConfiguration>(configuration, ApplicationConfiguration.SectionName)
			.AddSingleton<IApplicationNameProvider, ApplicationNameProvider>();
}
