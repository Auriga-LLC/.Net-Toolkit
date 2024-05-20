using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// Extension methods for setting up resilience for Http clients in an <see cref="IServiceCollection"/>.
/// </summary>
public static class HttpClientBuilderExtensions
{
	public static IHttpClientBuilder SetupHttpRequestResilence(
		this IHttpClientBuilder clientBuilder,
		HttpConnectionOptions clientConnectionOptions)
	{
		ArgumentNullException.ThrowIfNull(clientConnectionOptions);

		// setup resilience, if requested
		if (!clientConnectionOptions.Resilience.Enabled)
		{
			return clientBuilder;
		}

		return clientBuilder
			.AddTransientHttpErrorPolicy(policy =>
				policy.Or<HttpRequestException>()
				.WaitAndRetryAsync(clientConnectionOptions.Resilience.RetryCount,
				_ => clientConnectionOptions.Resilience.Timeout));
	}
}
