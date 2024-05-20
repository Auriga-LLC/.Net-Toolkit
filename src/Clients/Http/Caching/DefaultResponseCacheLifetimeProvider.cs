using Microsoft.Extensions.Options;
using Auriga.Toolkit.Caching;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// 
/// </summary>
/// <param name="cacheConfigurationOptions"></param>
internal sealed class DefaultResponseCacheLifetimeProvider(IOptions<CacheStorageConnectionOptions> cacheConfigurationOptions)
	: IResponseCacheLifetimeProvider
{
	/// <inheritdoc/>
	public ValueTask<TimeSpan> GetCacheTimeoutAsync(string content, CancellationToken cancellationToken = default)
	{
		return ValueTask.FromResult(cacheConfigurationOptions.Value.TimeOut);
	}
}
