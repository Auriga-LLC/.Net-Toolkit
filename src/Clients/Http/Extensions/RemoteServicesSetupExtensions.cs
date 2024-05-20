using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// Extension methods for setting up RemoteServices in an <see cref="IServiceCollection"/>.
/// </summary>
public static class RemoteServicesSetupExtensions
{
	/// <summary>
	/// Setup specific service client in the specified <see cref="IServiceCollection"/>.
	/// </summary>
	/// <typeparam name="TClient">Remote service client contract.</typeparam>
	/// <typeparam name="TImplementation">Remote service client implementation.</typeparam>
	/// <typeparam name="TConfiguration">Service configuration options type to register.</typeparam>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <param name="sectionName">Residential service specific section name.</param>
	/// <exception cref="ArgumentNullException">There is no required services."</exception>
	/// <exception cref="InvalidOperationException"></exception>
	/// <returns>The <see cref="IHttpClientBuilder"/> so that additional calls can be chained.</returns>
	public static IHttpClientBuilder SetupHttpServiceClient<TClient, TImplementation, TConfiguration>(
		this IServiceCollection services,
		IConfiguration configuration,
		string sectionName)
		where TClient : class
		where TImplementation : class, TClient
		where TConfiguration : HttpConnectionOptions
	{
		ArgumentNullException.ThrowIfNull(configuration);

		TConfiguration remoteServiceConfiguration = configuration.GetConfiguration<TConfiguration>(sectionName)
			?? throw new InvalidOperationException(
				ConfigurationLogMessages.SettingsSetupNotFound.FormatInterpolatedString(sectionName));
		_ = services.ConfigureOptions<TConfiguration>(configuration, sectionName);
		return SetupHttpServiceClient<TClient, TImplementation>(services, remoteServiceConfiguration);
	}

	/// <summary>
	/// Setup specific service client in the specified <see cref="IServiceCollection"/>.
	/// </summary>
	/// <typeparam name="TClient">Remote service client contract.</typeparam>
	/// <typeparam name="TImplementation">Remote service client implementation.</typeparam>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="clientConnectionOptions">Service client configuration.</param>
	/// <exception cref="ArgumentNullException">There is no required services."</exception>
	/// <exception cref="InvalidOperationException"></exception>
	/// <returns>The <see cref="IHttpClientBuilder"/> so that additional calls can be chained.</returns>
	public static IHttpClientBuilder SetupHttpServiceClient<TClient, TImplementation>(
		this IServiceCollection services,
		HttpConnectionOptions clientConnectionOptions)
		where TClient : class
		where TImplementation : class, TClient
	{
		ArgumentNullException.ThrowIfNull(clientConnectionOptions);

		IHttpClientBuilder serviceClientBuilder = services.TryAddHttpClient<TClient, TImplementation>(clientConnectionOptions)
			?? throw new InvalidOperationException();
		services.TryAddSingleton<IHttpResponseParser, JsonHttpResponseParser>();

		// Add app name to outgoing requests
		_ = serviceClientBuilder.AddHttpMessageHandler(serviceProvider =>
			new RequestExecutorDelegatingHandler(
				serviceProvider.GetRequiredService<ILogger<RequestExecutorDelegatingHandler>>(),
				serviceProvider.GetRequiredService<IApplicationNameProvider>()));

		return serviceClientBuilder
			.SetupHttpResponseCaching<TClient, TImplementation>(services, clientConnectionOptions)
			.SetupHttpAuthentication<TClient, TImplementation>(services, clientConnectionOptions)
			.SetupHttpTracing<TClient, TImplementation>(clientConnectionOptions)
			.SetupHttpRequestResilence(clientConnectionOptions);
	}

	public static IHttpClientBuilder SetupHttpTracing<TClient, TImplementation>(
		this IHttpClientBuilder clientBuilder,
		HttpConnectionOptions clientConnectionOptions)
		where TClient : class
		where TImplementation : class, TClient
	{
		ArgumentNullException.ThrowIfNull(clientConnectionOptions);

		// setup verbose logging, if requested
		if (!clientConnectionOptions.RequestTracing.Enabled)
		{
			return clientBuilder;
		}

		return clientBuilder.AddHttpMessageHandler(serviceProvider =>
			new TraceLogDelegatingHandler(serviceProvider.GetRequiredService<ILogger<TImplementation>>(), clientConnectionOptions));
	}
}
