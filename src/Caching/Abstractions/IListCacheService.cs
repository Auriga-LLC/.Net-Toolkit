namespace Auriga.Toolkit.Caching;

/// <summary>
/// Generic "List" cache storage service contract.
/// </summary>
/// <typeparam name="TStorage">Storage key type used to store/retrieve data from cache.</typeparam>
/// <typeparam name="TKey">Key type to be stored/retrieved from cache.</typeparam>
/// <typeparam name="TValue">Value type to be stored/retrieved from cache.</typeparam>
public interface IListCacheService<in TStorage, in TKey, TValue> : IBasicCacheService<TStorage, TKey>
{
	/// <summary>
	/// Asynchronously checks is value contained in cache.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="value">Value to be checked in storage.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous request operation.</returns>
	ValueTask<bool> HasValueAsync(TStorage storage, TKey key, TValue value, CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets object values from cache asynchronously.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous request operation.</returns>
	ValueTask<IEnumerable<TValue>> GetValuesAsync(TStorage storage, TKey key, CancellationToken cancellationToken = default);

	/// <summary>
	/// Saves object and stores it to cache asynchronously.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="values">Values to store.</param>
	/// <param name="expiry">Key expiration time.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous operation.</returns>
	ValueTask SaveValuesAsync(
		TStorage storage,
		TKey key,
		IEnumerable<TValue> values,
		TimeSpan? expiry = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes value from set by key in store asynchronously.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="value">Value to be deleted.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous delete operation.</returns>
	ValueTask DeleteValueAsync(TStorage storage, TKey key, TValue value, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes values from set by key in store asynchronously.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="values">Values to be deleted.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous delete operation.</returns>
	ValueTask DeleteValuesAsync(TStorage storage, TKey key, IEnumerable<TValue> values, CancellationToken cancellationToken = default);
}
