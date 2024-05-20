using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.Caching;

/// <summary>
/// Caching feature plugin.
/// </summary>
internal sealed class CacheFeaturePlugin : FeaturePlugin, IConfigurationCheckerPlugin, IServiceConfiguratorPlugin
{
	private CacheStorageConnectionOptions? _cacheConfiguration;

	///<inheritdoc/>
	public override string Name => CachePluginConstants.FeatureName;

	///<inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.CachePlugin;

	///<inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		_cacheConfiguration = configuration.GetConfiguration<CacheStorageConnectionOptions>(CacheStorageConnectionOptions.SectionName);
		Enabled = _cacheConfiguration != null;
		return configuration;
	}

	///<inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		if (!Enabled)
		{
			return services;
		}

		_ = services.ConfigureOptions<CacheStorageConnectionOptions>(configuration, CacheStorageConnectionOptions.SectionName);

		return _cacheConfiguration!.Type switch
		{
			CacheStorageType.Redis => services.SetupRedis(),
			_ => throw new ArgumentOutOfRangeException(nameof(_cacheConfiguration.Type))
		};
	}
}
