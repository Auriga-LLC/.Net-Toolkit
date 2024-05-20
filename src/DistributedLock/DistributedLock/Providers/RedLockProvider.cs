using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedLockNet;
using Auriga.Toolkit.DistributedLock.Abstractions;
using Auriga.Toolkit.DistributedLock.Abstractions.Extensions;
using Auriga.Toolkit.DistributedLock.Models;
using Auriga.Toolkit.DistributedLock.Models.Configuration;

namespace Auriga.Toolkit.DistributedLock.Providers;

/// <summary>
///
/// </summary>
/// <param name="_logger"></param>
/// <param name="_locker"></param>
/// <param name="_configuration"></param>
internal sealed class RedLockProvider(
	ILogger<RedLockProvider> _logger,
	IDistributedLockFactory _locker,
	IOptions<DistributedLockStorageClientConfiguration> _configuration) : IDistributedLockProvider
{
	/// <inheritdoc/>
	public IDistributedLock SetLock(string key, CancellationToken cancellationToken = default)
	{
		return SetLock(
			key,
			_configuration.Value.KeyExpiry,
			_configuration.Value.TimeOut,
			_configuration.Value.Resilience.Timeout,
			cancellationToken
		);
	}

	/// <inheritdoc/>
	public IDistributedLock SetLock(string key, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		_logger.LogLockRequested("sync", key, expiryTime, waitTime, retryTime);

		IRedLock redLock = _locker.CreateLock(key, expiryTime, waitTime, retryTime);
		return new RedisLockModel(redLock);
	}

	/// <inheritdoc/>
	public ValueTask<IDistributedLock> SetLockAsync(string key, CancellationToken cancellationToken = default)
	{
		return SetLockAsync(
			key,
			_configuration.Value.KeyExpiry,
			_configuration.Value.TimeOut,
			_configuration.Value.Resilience.Timeout,
			cancellationToken);
	}

	/// <inheritdoc/>
	public async ValueTask<IDistributedLock> SetLockAsync(string key, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime, CancellationToken cancellationToken = default)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(key);

		_logger.LogLockRequested("async", key, expiryTime, waitTime, retryTime);

		IRedLock redLock = await _locker.CreateLockAsync(key, expiryTime, waitTime, retryTime, cancellationToken)
			.ConfigureAwait(false);
		return new RedisLockModel(redLock);
	}
}
