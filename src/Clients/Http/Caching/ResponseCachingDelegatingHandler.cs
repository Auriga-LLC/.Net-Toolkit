using System.Net;
using Microsoft.Extensions.Logging;
using Auriga.Toolkit.Caching;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// 
/// </summary>
/// <param name="logger"></param>
/// <param name="cacheService"></param>
/// <param name="cacheLifetimeDeterminationService"></param>
internal sealed class ResponseCachingDelegatingHandler(
	ILogger<ResponseCachingDelegatingHandler> logger,
	IRecordCacheService cacheService,
	IResponseCacheLifetimeProvider cacheLifetimeDeterminationService) : DelegatingHandler
{
	/// <inheritdoc/>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		string cachedResponse = await cacheService.GetValueAsync(CacheDatabase.RequestsCacheDb, request.RequestUri!.AbsoluteUri, cancellationToken)
			.ConfigureAwait(false);
		if (!string.IsNullOrWhiteSpace(cachedResponse))
		{
			return new HttpResponseMessage
			{
				Content = new StringContent(cachedResponse)
			};
		}

		// Go get data from the http endpoint
		HttpResponseMessage response = await base.SendAsync(request, cancellationToken)
			.ConfigureAwait(false);
		if (response.StatusCode == HttpStatusCode.NoContent)
		{
			return response;
		}

		string content = await response.Content.ReadAsStringAsync(cancellationToken)
			.ConfigureAwait(false);
		TimeSpan cacheTimeout = await cacheLifetimeDeterminationService.GetCacheTimeoutAsync(content, cancellationToken)
			.ConfigureAwait(false);
		await cacheService.SaveValueAsync(CacheDatabase.RequestsCacheDb, request.RequestUri.AbsoluteUri, content, cacheTimeout, cancellationToken)
			.ConfigureAwait(false);
		return response;
	}
}
