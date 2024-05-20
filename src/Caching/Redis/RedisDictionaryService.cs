using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Auriga.Toolkit.Caching;

/// <summary>
/// Redis-specific dictionary-based implementation for cache service.
/// </summary>
internal sealed class RedisDictionaryService(
	IOptions<CacheStorageConnectionOptions> config,
	ILogger<RedisBasicCacheService> logger,
	ICacheClientProviderFactory providerFactory)
	: RedisBasicCacheService(config, logger, providerFactory), IDictionaryCacheService
{
	/// <inheritdoc/>
	public ValueTask<TReturnValue?> GetValueAsync<TReturnValue>(int storage, string key, string valueKey,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled<TReturnValue?>(cancellationToken);
		}

		return GetValueInternalAsync<TReturnValue>(storage, key, valueKey);
	}

	private async ValueTask<TReturnValue?> GetValueInternalAsync<TReturnValue>(int storage, string key, string valueKey)
	{
		RedisValue storageValue = await RedisClient.GetDatabase(storage)
			.HashGetAsync(key, valueKey)
			.ConfigureAwait(false);
		return JsonConvert.DeserializeObject<TReturnValue>(storageValue.ToString());
	}

	/// <inheritdoc/>
	public ValueTask SaveValuesAsync<TInputValue>(
		int storage,
		string key,
		string valueKey,
		TInputValue values,
		TimeSpan? expiry = null,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);
		ArgumentException.ThrowIfNullOrWhiteSpace(valueKey);
		ArgumentNullException.ThrowIfNull(values);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled(cancellationToken);
		}

		expiry ??= CacheConfiguration.KeyExpiry;

		return SaveValuesInternalAsync(storage, key, valueKey, values, expiry.Value);
	}

	private async ValueTask SaveValuesInternalAsync<TInputValue>(
		int storage,
		string key,
		string valueKey,
		TInputValue values,
		TimeSpan? expiry = null)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);
		HashEntry[] hashFields = [new HashEntry(valueKey, JsonConvert.SerializeObject(values))];
		Logger.LogSavingValuesToStorage(values, storage);
		await cacheStorage.HashSetAsync(
				key,
				hashFields,
				CommandFlags.FireAndForget)
			.ConfigureAwait(false);

		_ = await cacheStorage.KeyExpireAsync(
			key,
			expiry,
			CommandFlags.FireAndForget)
			.ConfigureAwait(false);
	}
}
