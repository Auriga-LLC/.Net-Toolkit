using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Auriga.Toolkit.Logging;

namespace Auriga.Toolkit.Caching;

/// <summary>
/// Redis connection provider.
/// </summary>
/// <param name="logger">Logger service.</param>
/// <param name="config">Cache configuration options.</param>
internal sealed class RedisConnectionProvider(
	ILogger<RedisConnectionProvider> logger,
	IOptions<CacheStorageConnectionOptions> config)
	: ICacheClientProviderFactory
{
	/// <inheritdoc/>
	public IConnectionMultiplexer CreateClient()
	{
		try
		{
			var options = new ConfigurationOptions
			{
				EndPoints = { config.Value.Endpoint!.ToString() },
				AbortOnConnectFail = !config.Value.Resilience.Enabled,
				ConnectRetry = config.Value.Resilience.RetryCount,
				AsyncTimeout = (int)config.Value.TimeOut.TotalMilliseconds,
				SyncTimeout = (int)config.Value.TimeOut.TotalMilliseconds,
			};

			if (config.Value.Authentication?.Enabled == true)
			{
				options.User = config.Value.Authentication.UserId;
				options.Password = config.Value.Authentication.UserSecret;
			}

			return ConnectionMultiplexer.Connect(options);
		}
		catch (RedisConnectionException ex)
		{
			logger.LogMethodFailed(nameof(CreateClient), ex);
			throw;
		}
	}
}
