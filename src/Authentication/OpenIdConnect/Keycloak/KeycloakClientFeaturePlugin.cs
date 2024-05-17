using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Extensions.Clients.Http;
using Toolkit.Extensions.Configuration;
using Toolkit.Extensions.Plugins;

namespace Toolkit.Extensions.Authentication.OpenIdConnect;

/// <summary>
/// Keycloak OpenID connect client feature plugin.
/// </summary>
internal sealed class KeycloakClientFeaturePlugin : FeaturePlugin, IConfigurationCheckerPlugin, IServiceConfiguratorPlugin
{
	/// <inheritdoc/>
	public override string Name => "Keycloak OIDC client";

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.PrerequisiteForHttpAuthentication;

	/// <inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		Enabled = configuration.GetConfigurationModel<OpenIdConnectServiceConnectionOptions>(OpenIdConnectServiceConnectionOptions.SectionName) != null;
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
			.AddSingleton<IWellKnownMetadataUrlProvider, KeycloakWellKnownMetadataUrlProvider>()
			.SetupHttpServiceClient<IOpenIdConnectAuthenticationService, KeycloakAuthenticationService, OpenIdConnectServiceConnectionOptions>(
				configuration,
				OpenIdConnectServiceConnectionOptions.SectionName);
		return services
			// Customize authentication data
			.AddKeyedTransient<IHttpServiceClientAuthenticationDataProvider, BasicAuthenticationDataProvider>(typeof(KeycloakAuthenticationService).FullName)
			.AddSingleton<IOpenIdConnectConfigurationProvider, KeycloakOpenIdConfigurationProvider>();
	}
}
