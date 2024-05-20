namespace Auriga.Toolkit.DistributedLock.Abstractions;

/// <summary>
/// Distributed locks provider contract.
/// </summary>
public interface IDistributedLockProvider
{
	/// <summary>
	/// Create shared lock synchronously.
	/// </summary>
	/// <param name="key">Key for creating lock.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <exception cref="ArgumentNullException">If lock key is empty.</exception>
	/// <returns>A <see cref="IDistributedLock"/> that represents the lock request operation.</returns>
	IDistributedLock SetLock(string key, CancellationToken cancellationToken = default);

	/// <summary>
	/// Create shared lock synchronously.
	/// </summary>
	/// <param name="key">Key for creating lock.</param>
	/// <param name="expiryTime">Expiry timer.</param>
	/// <param name="waitTime">Await timer.</param>
	/// <param name="retryTime">Retry timer.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <exception cref="ArgumentNullException">If lock key is empty.</exception>
	/// <returns>A <see cref="IDistributedLock"/> that represents the lock request operation.</returns>
	IDistributedLock SetLock(string key, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime, CancellationToken cancellationToken = default);

	/// <summary>
	/// Create shared lock asynchronously.
	/// </summary>
	/// <param name = "key" > Key for creating lock.</param>
	/// <param name = "cancellationToken" > The token to monitor for cancellation requests.The default value is <see cref ="CancellationToken.None"/>.</param>
	/// <exception cref = "ArgumentNullException" > If lock key is empty.</exception>
	/// <returns>A <see cref = "Task"/> that represents the asynchronous request operation.</returns>
	ValueTask<IDistributedLock> SetLockAsync(string key, CancellationToken cancellationToken = default);

	/// <summary>
	/// Create shared lock asynchronously.
	/// </summary>
	/// <param name = "key" > Key for creating lock.</param>
	/// <param name = "expiryTime" > Expiry timer.</param>
	/// <param name = "waitTime" > Await timer.</param>
	/// <param name = "retryTime" > Retry timer.</param>
	/// <param name = "cancellationToken" > The token to monitor for cancellation requests.The default value is <see cref ="CancellationToken.None"/>.</param>
	/// <exception cref = "ArgumentNullException" > If lock key is empty.</exception>
	/// <returns>A<see cref = "Task"/> that represents the asynchronous request operation.</returns>
	ValueTask<IDistributedLock> SetLockAsync(string key, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime, CancellationToken cancellationToken = default);
}
