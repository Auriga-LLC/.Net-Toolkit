using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using Auriga.Toolkit.Caching;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.DistributedLock.Abstractions;
using Auriga.Toolkit.DistributedLock.Models.Configuration;
using Auriga.Toolkit.DistributedLock.Providers;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.DistributedLock;

internal sealed class DistributedLockFeaturePlugin : FeaturePlugin, IConfigurationCheckerPlugin, IServiceConfiguratorPlugin
{
	private DistributedLockStorageClientConfiguration? _configuration;

	/// <inheritdoc/>
	public override string Name => "DistributedLock Redlock Plugin";

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.DistributedLockPlugin;

	/// <inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		_configuration = configuration.GetConfiguration<DistributedLockStorageClientConfiguration>(DistributedLockStorageClientConfiguration.SectionName);
		Enabled = _configuration!= null;
		return configuration;
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		if (!Enabled)
		{
			return services;
		}

		_ = services.ConfigureOptions<DistributedLockStorageClientConfiguration>(configuration, DistributedLockStorageClientConfiguration.SectionName);

		switch (_configuration!.Type)
		{
			case CacheStorageType.Redis:
			{
				services.TryAddSingleton<IDistributedLockFactory>(sp =>
				{
					ICacheClientProviderFactory redis = sp.GetRequiredService<ICacheClientProviderFactory>();
					var redLockMultiplexer = new RedLockMultiplexer(redis.CreateClient())
					{
						RedisDatabase = CacheDatabase.DistributedLockCacheDb,
						RedisKeyFormat = $"{CacheDatabase.DistributedLockCacheDb}::"
					};

					return RedLockFactory.Create(
						new List<RedLockMultiplexer> {redLockMultiplexer},
						new RedLockRetryConfiguration(_configuration.Resilience.RetryCount, (int?)_configuration.Resilience.Timeout.TotalMilliseconds)
					);
				});
				services.TryAddSingleton<IDistributedLockProvider, RedLockProvider>();
			} break;

			default:
			{
				throw new NotImplementedException();
			}
		}

		return services;
	}
}
