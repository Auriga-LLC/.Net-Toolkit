namespace Auriga.Toolkit.Caching;

/// <summary>
/// Basic cache storage service contract.
/// </summary>
/// <typeparam name="TStorage">Storage key type used to store/retrieve data from cache.</typeparam>
/// <typeparam name="TKey">Key type to be stored/retrieved from cache.</typeparam>
public interface IBasicCacheService<in TStorage, in TKey> : IDisposable
{
	/// <summary>
	/// Asynchronously checks is key in store.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous value read operation.</returns>
	ValueTask<bool> HasKeyAsync(TStorage storage, TKey key, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes value by key in store asynchronously.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous delete operation.</returns>
	ValueTask DeleteKeyAsync(TStorage storage, TKey key, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes keys in store asynchronously.
	/// </summary>
	/// <param name="storage">Cache storage.</param>
	/// <param name="keys">Keys in storage.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous delete operation.</returns>
	ValueTask DeleteKeysAsync(TStorage storage, IEnumerable<TKey> keys, CancellationToken cancellationToken = default);
}
