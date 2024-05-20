namespace Auriga.Toolkit.Caching;

/// <summary>
/// Generic "Dictionary" cache storage service contract.
/// </summary>
/// <typeparam name="TStorage">Storage key type used to store/retrieve data from cache.</typeparam>
/// <typeparam name="TKey">Key type to be stored/retrieved from cache.</typeparam>
/// <typeparam name="TValue">Value type to be stored/retrieved from cache.</typeparam>
public interface IDictionaryCacheService<in TStorage, in TKey, in TValue> : IBasicCacheService<TStorage, TKey>
{
	/// <summary>
	/// Gets object values from cache asynchronously.
	/// </summary>
	/// <typeparam name="TReturnValue">Requested return type.</typeparam>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="valueKey">Key in dictionary.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous request operation.</returns>
	ValueTask<TReturnValue?> GetValueAsync<TReturnValue>(
		TStorage storage,
		TKey key,
		TValue valueKey,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Saves object and stores it to cache asynchronously.
	/// </summary>
	/// <typeparam name="TInputValue">Requested input type.</typeparam>
	/// <param name="storage">Cache storage.</param>
	/// <param name="key">Key in storage.</param>
	/// <param name="valueKey">Key in hash.</param>
	/// <param name="values">Values to store.</param>
	/// <param name="expiry">Key expiration time.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous operation.</returns>
	ValueTask SaveValuesAsync<TInputValue>(
		TStorage storage,
		TKey key,
		TValue valueKey,
		TInputValue values,
		TimeSpan? expiry = null,
		CancellationToken cancellationToken = default);
}
