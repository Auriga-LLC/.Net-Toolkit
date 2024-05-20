using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Caching;

/// <summary>
/// List of well known cache databases.
/// </summary>
/// <remarks>
/// Used in cooperation with <see cref="CacheStorageType"/>.<br/>
/// If we use <see cref="CacheStorageType.Redis"/> we can use only 16 databases <see href="https://github.com/StackExchange/StackExchange.Redis/issues/1071"/>.
/// </remarks>
[ExcludeFromCodeCoverage]
public static class CacheDatabase
{
	/// <summary>
	/// Requests response cache database.
	/// </summary>
	public const int RequestsCacheDb = 1;

	/// <summary>
	/// Distributed lock cache database.
	/// </summary>
	/// <remarks>Used to store distributed locks info.</remarks>
	public const int DistributedLockCacheDb = 3;
}
