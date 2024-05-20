using Microsoft.Extensions.DependencyInjection;
using Auriga.Toolkit.Http;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// Http client extension methods
/// </summary>
public static class HttpClientSetupExtensions
{
	/// <summary>
	/// Safely registers typed HttpClient once.
	/// </summary>
	/// <typeparam name="TClient">Remote service client contract.</typeparam>
	/// <typeparam name="TImplementation">Remote service client implementation.</typeparam>
	/// <remarks>Exists because of bug <see href="https://github.com/dotnet/extensions/issues/3158"/> and <see href="https://github.com/Azure/azure-functions-host/issues/5098"/>.</remarks>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="connectionConfiguration">Connection configuration model.</param>
	/// <param name="serviceClientName">Application client name.</param>
	/// <returns>The <see cref="IHttpClientBuilder"/> so that additional calls can be chained.</returns>
	public static IHttpClientBuilder? TryAddHttpClient<TClient, TImplementation>(
		this IServiceCollection services,
		HttpConnectionOptions connectionConfiguration,
		string? serviceClientName = null)
		where TClient : class
		where TImplementation : class, TClient
	{
		if (services.Any(x => x.ServiceType == typeof(TClient)))
		{
			return null;
		}

		serviceClientName ??= typeof(TImplementation).Name;

		return services.AddHttpClient<TClient, TImplementation>(
			serviceClientName,
			clientConfig =>
			{
				clientConfig.BaseAddress = connectionConfiguration.Endpoint;
				clientConfig.Timeout = connectionConfiguration.TimeOut;
				clientConfig.DefaultRequestHeaders.UserAgent.Add(typeof(TImplementation).BuildUserAgentHeader(serviceClientName));
				clientConfig.DefaultRequestHeaders.Add(
					HeaderName.RequestAuthorizationMode, connectionConfiguration.Authentication?.Type.GetDescription());
			});
	}
}
