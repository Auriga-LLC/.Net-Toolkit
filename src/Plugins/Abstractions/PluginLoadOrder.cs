namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Stores predefined load order for plugin groups.
/// </summary>
public enum PluginLoadOrder
{
	/// <summary>
	/// Startup critical plugins group.
	/// </summary>
	StartupCritical = 0,

	/// <summary>
	/// Application configuration plugins group.
	/// </summary>
	ConfigurationCritical = 1,

	/// <summary>
	/// Independent plugins group.
	/// </summary>
	Independent = 50,

	/// <summary>
	/// HTTP server prerequisite plugins group.
	/// </summary>
	PrerequisiteForHttpServer = DependsOnHttpServer - 1,

	/// <summary>
	/// HTTP dependant plugins group.
	/// </summary>
	DependsOnHttpServer = 100,

	/// <summary>
	/// HTTP authentication prerequisite plugins group.
	/// </summary>
	PrerequisiteForHttpAuthentication = HttpAuthentication - 1,

	/// <summary>
	/// Http Authentication plugins group.
	/// </summary>
	HttpAuthentication = DependsOnHttpServer + 100,

	/// <summary>
	/// HTTP Authentication dependant plugins group.
	/// </summary>
	DependsOnHttpAuthentication = HttpAuthentication + 1,

	/// <summary>
	/// HTTP authorization prerequisite plugins group.
	/// </summary>
	PrerequisiteForHttpAuthorization = HttpAuthorization - 1,

	/// <summary>
	/// Http authorization plugins group.
	/// </summary>
	HttpAuthorization = DependsOnHttpAuthentication + 100,

	/// <summary>
	/// HTTP authorization dependant plugins group.
	/// </summary>
	DependsOnHttpAuthorization = HttpAuthorization + 1,

	/// <summary>
	/// Caching plugins group.
	/// </summary>
	CachePlugin = 400,

	/// <summary>
	/// Cache dependant plugins group.
	/// </summary>
	DependsOnCachePlugin = CachePlugin + 1,

	/// <summary>
	/// Distributed lock plugins group.
	/// </summary>
	DistributedLockPlugin = DependsOnCachePlugin,

	/// <summary>
	/// Distributed lock dependant plugins group.
	/// </summary>
	DependsOnDistributedLockPlugin = DistributedLockPlugin + 1,

	/// <summary>
	/// Kafka/RabbitMQ/etc event bus plugins group.
	/// </summary>
	MessageBusPlugin = 500,

	/// <summary>
	/// Event bus dependant plugins group.
	/// </summary>
	DependsOnMessageBusPlugin = MessageBusPlugin + 1,

	/// <summary>
	/// Database plugins group.
	/// </summary>
	DatabasePlugin = 600,

	/// <summary>
	/// Database dependant plugins group.
	/// </summary>
	DependsOnDatabasePlugin = DatabasePlugin + 1,

	/// <summary>
	/// Application level plugins group
	/// </summary>
	ApplicationLevelPlugin =
		DependsOnHttpAuthentication + DependsOnHttpAuthorization
		+ DependsOnCachePlugin + DependsOnDistributedLockPlugin
		+ DependsOnMessageBusPlugin
		+ DependsOnDatabasePlugin
}
