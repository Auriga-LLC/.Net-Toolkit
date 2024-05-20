using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Auriga.Toolkit.Caching;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// Extension methods for setting up caching for Http clients in an <see cref="IServiceCollection"/>.
/// </summary>
public static class HttpClientBuilderExtensions
{
	public static IHttpClientBuilder SetupHttpResponseCaching<TClient, TImplementation>(
		this IHttpClientBuilder clientBuilder,
		IServiceCollection services,
		HttpConnectionOptions clientConnectionOptions)
		where TClient : class
		where TImplementation : class, TClient
	{
		ArgumentNullException.ThrowIfNull(clientConnectionOptions);

		if (clientConnectionOptions.ResponseCaching?.Enabled != true)
		{
			return clientBuilder;
		}

		services.TryAddTransient<IResponseCacheLifetimeProvider, DefaultResponseCacheLifetimeProvider>();

		return clientBuilder.AddHttpMessageHandler(serviceProvider =>
			new ResponseCachingDelegatingHandler(
				serviceProvider.GetRequiredService<ILogger<ResponseCachingDelegatingHandler>>(),
				serviceProvider.GetRequiredService<IRecordCacheService>(),
				serviceProvider.GetNamedServiceOrDefault<IResponseCacheLifetimeProvider>(typeof(TImplementation).FullName)));
	}
}
