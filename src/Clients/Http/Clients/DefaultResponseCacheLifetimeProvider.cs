using Microsoft.Extensions.Options;
using Toolkit.Extensions.Caching.Abstractions.Configuration;

namespace Toolkit.Extensions.Clients.Http;

public class DefaultResponseCacheLifetimeProvider(IOptions<CacheStorageClientConfiguration> cacheConfigurationOptions)
	: IResponseCacheLifetimeProvider
{
	/// <inheritdoc/>
	public ValueTask<TimeSpan> GetCacheTimeoutAsync(string content, CancellationToken cancellationToken = default)
	{
		return ValueTask.FromResult(cacheConfigurationOptions.Value.TimeOut);
	}
}
