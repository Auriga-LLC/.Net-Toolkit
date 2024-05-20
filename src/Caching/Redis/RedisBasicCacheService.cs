using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Auriga.Toolkit.Caching;

/// <summary>
/// Redis-specific base implementation for cache service.
/// </summary>
internal class RedisBasicCacheService : IBasicCacheService
{
	private bool _alreadyDisposed;
	protected CacheStorageConnectionOptions CacheConfiguration { get; }
	protected ILogger<RedisBasicCacheService> Logger { get; }
	protected IConnectionMultiplexer RedisClient { get; }

	public RedisBasicCacheService(
		IOptions<CacheStorageConnectionOptions> config,
		ILogger<RedisBasicCacheService> logger,
		ICacheClientProviderFactory providerFactory)
	{
		ArgumentNullException.ThrowIfNull(config);
		ArgumentNullException.ThrowIfNull(logger);
		ArgumentNullException.ThrowIfNull(providerFactory);

		CacheConfiguration = config.Value;
		Logger = logger;
		RedisClient = providerFactory.CreateClient();
	}

	/// <inheritdoc/>
	public ValueTask<bool> HasKeyAsync(int storage, string key, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled<bool>(cancellationToken);
		}

		return HasKeyInternalAsync(storage, key);
	}

	private ValueTask<bool> HasKeyInternalAsync(int storage, string key)
		=> new(RedisClient.GetDatabase(storage).KeyExistsAsync(key));

	/// <inheritdoc/>
	public ValueTask DeleteKeyAsync(int storage, string key, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		if (cancellationToken.IsCancellationRequested)
		{
			return ValueTask.FromCanceled(cancellationToken);
		}

		return DeleteKeyInternalAsync(storage, key);
	}

	private async ValueTask DeleteKeyInternalAsync(int storage, string key)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);

		Logger.LogDeletingKeyFromStorage(key, storage);

		_ = await cacheStorage.KeyDeleteAsync(key, CommandFlags.FireAndForget)
			.ConfigureAwait(false);
	}

	/// <inheritdoc/>
	public ValueTask DeleteKeysAsync(int storage, IEnumerable<string> keys, CancellationToken cancellationToken = default)
	{
		string[] keysToDelete = keys.ToArray();
		if (keysToDelete.Length == 0)
		{
			throw new ArgumentNullException(nameof(keys));
		}

		if (cancellationToken.IsCancellationRequested)
		{
			Logger.LogCancelledWithKeys(nameof(DeleteKeysAsync), storage, keysToDelete);
			return ValueTask.FromCanceled(cancellationToken);
		}

		return DeleteKeysInternalAsync(storage, keysToDelete);
	}

	private async ValueTask DeleteKeysInternalAsync(int storage, string[] keys)
	{
		IDatabase cacheStorage = RedisClient.GetDatabase(storage);
		Logger.LogDeletingKeysFromStorage(keys, storage);
		_ = await cacheStorage.KeyDeleteAsync(keys.Select(key => (RedisKey)key).ToArray(), CommandFlags.FireAndForget)
			.ConfigureAwait(false);
	}

	/// <inheritdoc cref="IDisposable.Dispose()"/>
	protected virtual void Dispose(bool disposing)
	{
		if (_alreadyDisposed)
		{
			return;
		}

		if (disposing)
		{
			RedisClient.Close();
			RedisClient.Dispose();
		}

		_alreadyDisposed = true;
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
