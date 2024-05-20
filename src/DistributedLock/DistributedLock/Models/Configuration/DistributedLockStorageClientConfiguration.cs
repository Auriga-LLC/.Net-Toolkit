using Auriga.Toolkit.Caching;
using Auriga.Toolkit.Clients.Http;

namespace Auriga.Toolkit.DistributedLock.Models.Configuration;

/// <summary>
/// Configuration model for distributed lock functionality.
/// </summary>
/// <remarks>Strongly relies on cache connection.</remarks>
public sealed class DistributedLockStorageClientConfiguration : CacheStorageConnectionOptions
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public new const string SectionName = $"{ClientConnectionOptions.SectionName}:DistributedLock";
}
