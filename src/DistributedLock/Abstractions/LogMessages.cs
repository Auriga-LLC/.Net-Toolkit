namespace Auriga.Toolkit.DistributedLock.Abstractions;

internal static class DistributedLockLogMessages
{
	internal const string LockNotAcquired = "Distributed lock \"{lockName}\" is not acquired";
	internal const string LockAcquired = "Distributed lock \"{lockName}\" is acquired";

	internal const string LockRequested = "Requested {mode}hronous Distributed lock \"{lockKey}\" with expiry ({expiryTime}), wait ({waitTime}), retry ({retryTime})";
}
