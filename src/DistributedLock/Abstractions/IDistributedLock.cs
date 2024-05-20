namespace Auriga.Toolkit.DistributedLock.Abstractions;

/// <summary>
/// Distributed lock entry contract.
/// </summary>
public interface IDistributedLock : IDisposable, IAsyncDisposable
{
	/// <summary>
	/// Gets is lock acquired.
	/// </summary>
	bool IsAcquired { get; }
}
