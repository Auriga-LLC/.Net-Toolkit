namespace Auriga.Toolkit.Caching;

/// <summary>
/// Generic "Key-Value" cache storage service contract.
/// </summary>
/// <typeparam name="TStorage">Storage key type used to store/retrieve data from cache.</typeparam>
/// <typeparam name="TKey">Key type to be stored/retrieved from cache.</typeparam>
/// <typeparam name="TValue">Value type to be stored/retrieved from cache.</typeparam>
public interface IRecordCacheService<in TStorage, in TKey, TValue> : IBasicCacheService<TStorage, TKey>
{
	/// <summary>
	/// Gets single value from cache asynchronously.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous value read operation.</returns>
	ValueTask<TValue> GetValueAsync(TStorage storage, TKey key, CancellationToken cancellationToken = default);

	/// <summary>
	/// Stores value in cache asynchronously.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="value">Value to store.</param>
	/// <param name="expiry">Key expiration time.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous value save operation.</returns>
	ValueTask SaveValueAsync(
		TStorage storage,
		TKey key,
		TValue value,
		TimeSpan? expiry = null,
		CancellationToken cancellationToken = default);
}
