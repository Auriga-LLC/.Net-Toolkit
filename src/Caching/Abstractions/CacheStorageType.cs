namespace Auriga.Toolkit.Caching;

/// <summary>
/// Cache storage types.
/// </summary>
/// <remarks>Each type requires own provider implementation.</remarks>
public enum CacheStorageType
{
	/// <summary>
	/// Redis-like cache storage.
	/// </summary>
	/// <remarks>Recommended to use, by default.</remarks>
	Redis = 0,

	/// <summary>
	/// In-memory cache storage.
	/// </summary>
	/// <remarks>Should not be used, if application can be scaled.</remarks>
	InMemory = 1,

	/// <summary>
	/// Database cache storage.
	/// </summary>
	Database = 2
}
