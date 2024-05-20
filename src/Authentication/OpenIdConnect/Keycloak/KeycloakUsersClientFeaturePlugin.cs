using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auriga.Toolkit.Clients.Http;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.Authentication.OpenIdConnect;

/// <summary>
/// Keycloak OpenID connect client feature plugin.
/// </summary>
internal sealed class KeycloakUsersClientFeaturePlugin : FeaturePlugin, IConfigurationCheckerPlugin, IServiceConfiguratorPlugin
{
	/// <inheritdoc/>
	public override string Name => "Keycloak users client";

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.DependsOnHttpAuthentication;

	/// <inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		Enabled = configuration.GetConfiguration<OpenIdConnectServiceConnectionOptions>(OpenIdConnectServiceConnectionOptions.SectionName) != null;
		return configuration;
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		if (!Enabled)
		{
			return services;
		}

		_ = services
			.SetupHttpServiceClient<IKeycloakUsersServiceClient, KeycloakUsersService, OpenIdConnectServiceConnectionOptions>(
				configuration,
				OpenIdConnectServiceConnectionOptions.SectionName);
		return services
			// Customize authentication data
			.AddKeyedTransient<IHttpServiceClientAuthenticationDataProvider, ImpersonationLoginService>(typeof(KeycloakUsersService).FullName)
			.AddSingleton<IOpenIdConnectConfigurationProvider, KeycloakOpenIdConfigurationProvider>();
	}
}
