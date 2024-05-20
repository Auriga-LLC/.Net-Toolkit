using System.Diagnostics.CodeAnalysis;
using Auriga.Toolkit.Clients.Http;

namespace Auriga.Toolkit.Caching;

/// <summary>
/// Configuration model for cache storage client.
/// </summary>
[ExcludeFromCodeCoverage]
public class CacheStorageConnectionOptions : ClientConnectionOptions
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public new const string SectionName = "Caching";

	/// <summary>
	/// Gets cache storage type.
	/// </summary>
	/// <remarks>Requires specific implementation. Currently implemented only for Redis-like storages.</remarks>
	/// <value><see cref="CacheStorageType.Redis"/> by default.</value>
	public CacheStorageType Type { get; init; } = CacheStorageType.Redis;

	/// <summary>
	/// Gets key expiry time.
	/// </summary>
	/// <value><c>300</c> seconds by default.</value>
	public TimeSpan KeyExpiry { get; init; } = TimeSpan.FromSeconds(300);
}
