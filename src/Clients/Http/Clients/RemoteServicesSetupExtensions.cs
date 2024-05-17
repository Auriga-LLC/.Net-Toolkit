using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Polly;
using Toolkit.Extensions.Caching.Abstractions.Services;
using Toolkit.Extensions.Configuration;
using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Clients.Http;

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
		where TConfiguration : ClientConnectionOptions
	{
		ArgumentNullException.ThrowIfNull(configuration);

		TConfiguration remoteServiceConfiguration = services.RegisterConfigurationIfExists<TConfiguration>(
			configuration,
			sectionName)
			?? throw new InvalidOperationException(
				ConfigurationLogMessages.SettingsSetupNotFound.FormatInterpolatedString(sectionName));
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
		ClientConnectionOptions clientConnectionOptions)
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
			.SetupHttpServiceClientCaching<TClient, TImplementation>(services, clientConnectionOptions)
			.SetupHttpServiceClientAuthorization<TClient, TImplementation>(services, clientConnectionOptions)
			.SetupHttpServiceClientTracing<TClient, TImplementation>(clientConnectionOptions)
			.SetupHttpServiceClientResilence(clientConnectionOptions);
	}

	public static IHttpClientBuilder SetupHttpServiceClientAuthorization<TClient, TImplementation>(
		this IHttpClientBuilder clientBuilder,
		IServiceCollection services,
		ClientConnectionOptions clientConnectionOptions)
		where TClient : class
		where TImplementation : class, TClient
	{
		ArgumentNullException.ThrowIfNull(clientConnectionOptions);

		// Setup auth policy, if requested
		if (clientConnectionOptions.Authentication?.Enabled != true)
		{
			return clientBuilder;
		}

		switch (clientConnectionOptions.Authentication.Type)
		{
			case ClientAuthenticationType.Impersonation:
				services.TryAddTransient<IHttpServiceClientAuthenticationDataProvider, ImpersonationLoginService>();
				break;

			case ClientAuthenticationType.UserPassword:
				services.TryAddTransient<IHttpServiceClientAuthenticationDataProvider, UserPasswordLoginService>();
				break;

			case ClientAuthenticationType.OfflineToken:
				services.TryAddTransient<IHttpServiceClientAuthenticationDataProvider, OfflineTokenLoginService>();
				break;

			default:
				{
					throw new InvalidOperationException(
						"Tried to call not supported mode \"{configMode}\"".FormatInterpolatedString(
						clientConnectionOptions.Authentication.Type.ToString()));
				}
		}

		return clientBuilder.AddHttpMessageHandler(serviceProvider =>
			new AuthenticationDelegatingHandler(
				serviceProvider.GetRequiredService<ILogger<AuthenticationDelegatingHandler>>(),
				clientConnectionOptions.Authentication,
				serviceProvider.GetNamedServiceOrDefault<IHttpServiceClientAuthenticationDataProvider>(typeof(TImplementation).FullName)));
	}

	public static IHttpClientBuilder SetupHttpServiceClientCaching<TClient, TImplementation>(
		this IHttpClientBuilder clientBuilder,
		IServiceCollection services,
		ClientConnectionOptions clientConnectionOptions)
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

	public static IHttpClientBuilder SetupHttpServiceClientTracing<TClient, TImplementation>(
		this IHttpClientBuilder clientBuilder,
		ClientConnectionOptions clientConnectionOptions)
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

	public static IHttpClientBuilder SetupHttpServiceClientResilence(
		this IHttpClientBuilder clientBuilder,
		ClientConnectionOptions clientConnectionOptions)
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

	private static T GetNamedServiceOrDefault<T>(this IServiceProvider serviceProvider, string? serviceName)
		where T : notnull
	{
		return serviceProvider.GetKeyedService<T>(serviceName)
				?? serviceProvider.GetRequiredService<T>();
	}
}
