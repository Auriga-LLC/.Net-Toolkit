using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Auriga.Toolkit.Authentication.Abstractions;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// Extension methods for setting up authentication for Http clients in an <see cref="IServiceCollection"/>.
/// </summary>
public static class HttpClientBuilderExtensions
{
	public static IHttpClientBuilder SetupHttpAuthentication<TClient, TImplementation>(
		this IHttpClientBuilder clientBuilder,
		IServiceCollection services,
		HttpConnectionOptions clientConnectionOptions)
		where TClient : class
		where TImplementation : class, TClient
	{
		ArgumentNullException.ThrowIfNull(clientConnectionOptions);

		// Setup auth policy, if requested
		if (clientConnectionOptions.Authentication?.Enabled != true)
		{
			return clientBuilder;
		}

		if (clientConnectionOptions.Authentication.Type == ClientAuthenticationType.UserPassword)
		{
			services.TryAddTransient<IHttpServiceClientAuthenticationDataProvider, BasicAuthenticationDataProvider>();				
		}
		
		switch (clientConnectionOptions.Authentication.Scheme)
		{
			case AuthenticationScheme.Basic:
				services.TryAddTransient<IHttpServiceClientAuthenticationDataProvider, BasicAuthenticationDataProvider>();				
				break;
			
			case AuthenticationScheme.Bearer:
			{
				switch (clientConnectionOptions.Authentication.Mode)
				{
					case HttpTokenExchangeMode.Impersonation:
						services.TryAddTransient<IHttpServiceClientAuthenticationDataProvider, ImpersonationLoginService>();
						break;
		
					case HttpTokenExchangeMode.UserPassword:
						services.TryAddTransient<IHttpServiceClientAuthenticationDataProvider, UserPasswordLoginService>();
						break;
		
					case HttpTokenExchangeMode.OfflineToken:
						services.TryAddTransient<IHttpServiceClientAuthenticationDataProvider, OfflineTokenLoginService>();
						break;
				}
			} break;

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
}
