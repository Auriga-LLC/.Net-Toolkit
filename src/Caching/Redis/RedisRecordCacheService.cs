using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Auriga.Toolkit.Caching;

/// <summary>
/// Redis-specific record-based implementation for cache service.
/// </summary>
internal sealed class RedisRecordCacheService(
	IOptions<CacheStorageConnectionOptions> config,
	ILogger<RedisBasicCacheService> logger,
	ICacheClientProviderFactory providerFactory) : RedisBasicCacheService(config, logger, providerFactory), IRecordCacheService
{
	/// <inheritdoc/>
	public ValueTask<string> GetValueAsync(int storage, string key, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled<string>(cancellationToken);
		}

		return GetValueInternalAsync(storage, key);
	}

	private async ValueTask<string> GetValueInternalAsync(int storage, string key)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);
		return (await cacheStorage
			.StringGetAsync(key)
			.ConfigureAwait(false)).ToString();
	}

	/// <inheritdoc/>
	public ValueTask SaveValueAsync(int storage, string key, string value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);
		ArgumentException.ThrowIfNullOrWhiteSpace(value);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled(cancellationToken);
		}

		expiry ??= CacheConfiguration.KeyExpiry;

		return SaveValueInternalAsync(storage, key, value, expiry.Value);
	}

	private async ValueTask SaveValueInternalAsync(
		int storage,
		string key,
		string value,
		TimeSpan expiry)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);
		_ = await cacheStorage.StringSetAsync(
				key,
				value,
				expiry,
				When.Always,
				CommandFlags.FireAndForget)
			.ConfigureAwait(false);
	}
}
