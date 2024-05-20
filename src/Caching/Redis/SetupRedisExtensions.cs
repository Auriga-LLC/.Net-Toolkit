using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Auriga.Toolkit.Caching;

public static class SetupRedisExtensions
{
	public static IServiceCollection SetupRedis(this IServiceCollection services)
	{
		services.TryAddSingleton<ICacheClientProviderFactory, RedisConnectionProvider>();
		services.TryAddSingleton<IRecordCacheService, RedisRecordCacheService>();
		services.TryAddSingleton<IListCacheService, RedisListCacheService>();
		services.TryAddSingleton<IDictionaryCacheService, RedisDictionaryService>();
		return services;
	}
}
