using RedLockNet;
using Auriga.Toolkit.DistributedLock.Abstractions;

namespace Auriga.Toolkit.DistributedLock.Models;

/// <summary>
/// Red distributed lock entry wrapper.
/// </summary>
public sealed class RedisLockModel(IRedLock redLock) : IDistributedLock
{
	private bool _alreadyDisposed;

	/// <inheritdoc/>
	public bool IsAcquired => redLock.IsAcquired;

	private void Dispose(bool disposing)
	{
		if (!_alreadyDisposed)
		{
			return;
		}

		if (disposing)
		{
			redLock.Dispose();
		}

		_alreadyDisposed = true;
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <inheritdoc/>
	public ValueTask DisposeAsync() => redLock.DisposeAsync();
}
