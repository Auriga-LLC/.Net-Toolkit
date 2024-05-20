using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Auriga.Toolkit.Caching;

/// <summary>
/// Redis-specific list-based implementation for cache service.
/// </summary>
internal sealed class RedisListCacheService(
	IOptions<CacheStorageConnectionOptions> config,
	ILogger<RedisBasicCacheService> logger,
	ICacheClientProviderFactory providerFactory)
	: RedisBasicCacheService(config, logger, providerFactory), IListCacheService
{
	/// <inheritdoc/>
	public ValueTask<bool> HasValueAsync(int storage, string key, string value, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled<bool>(cancellationToken);
		}

		return HasValueInternalAsync(storage, key, value);
	}

	private ValueTask<bool> HasValueInternalAsync(int storage, string key, string value)
	{
		return new ValueTask<bool>(
			RedisClient.GetDatabase(storage).SetContainsAsync(key, value));
	}

	/// <inheritdoc/>
	public ValueTask<IEnumerable<string>> GetValuesAsync(int storage, string key, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled<IEnumerable<string>>(cancellationToken);
		}
		return GetValuesInternalAsync(storage, key);
	}

	private async ValueTask<IEnumerable<string>> GetValuesInternalAsync(int storage, string key)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);
		return (await cacheStorage
			.SetMembersAsync(key)
			.ConfigureAwait(false)).ToStringArray()!;
	}

	/// <inheritdoc/>
	public ValueTask SaveValuesAsync(int storage, string key, IEnumerable<string> values, TimeSpan? expiry = null,
		CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		string[] valuesArray = values.ToArray();
		if (valuesArray.Length == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(values));
		}

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled(cancellationToken);
		}

		expiry ??= CacheConfiguration.KeyExpiry;

		return SaveValuesInternalAsync(storage, key, valuesArray, expiry.Value);
	}

	private async ValueTask SaveValuesInternalAsync(
		int storage,
		string key,
		string[] values,
		TimeSpan expiry)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);

		_ = await cacheStorage.SetAddAsync(key, values.ToRedisValueArray(), CommandFlags.FireAndForget)
			.ConfigureAwait(false);

		_ = await cacheStorage.KeyExpireAsync(key, expiry, CommandFlags.FireAndForget)
			.ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public ValueTask DeleteValueAsync(int storage, string key, string value, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);
		ArgumentException.ThrowIfNullOrWhiteSpace(value);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled(cancellationToken);
		}

		return DeleteValueInternalAsync(storage, key, value);
	}

	private async ValueTask DeleteValueInternalAsync(int storage, string key, string value)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);
		_ = await cacheStorage.SetRemoveAsync(key, value, CommandFlags.FireAndForget)
			.ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public ValueTask DeleteValuesAsync(int storage, string key, IEnumerable<string> values, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		string[] valuesToDelete = values.ToArray();
		if (valuesToDelete.Length == 0)
		{
			throw new ArgumentNullException(nameof(values));
		}

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled(cancellationToken);
		}

		return DeleteValuesInternalAsync(storage, key, valuesToDelete);
	}

	private async ValueTask DeleteValuesInternalAsync(int storage, string key, string[] values)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);
		///Logger.LogInformation("Deleting values for key {Key} in storage {Storage}", key, storage);
		foreach (string value in values)
		{
			_ = await cacheStorage.KeyDeleteAsync(key, CommandFlags.FireAndForget).ConfigureAwait(false);
			///Logger.LogInformation("Deleted value {Value} for key {Key} in storage {Storage}", value, key, storage);
		}
	}
}
