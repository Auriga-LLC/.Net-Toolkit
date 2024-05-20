using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Auriga.Toolkit.Clients.Http;
using Auriga.Toolkit.Configuration;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Http authentication feature setup plugin.
/// </summary>
internal sealed class AuthenticationFeaturePlugin :
	FeaturePlugin,
	IConfigurationCheckerPlugin,
	IServiceConfiguratorPlugin,
	IPipelineConfiguratorPlugin
{
	private AuthenticationFeatureOptions? _authPolicy;

	/// <inheritdoc/>
	public override string Name => AuthenticationPluginConstants.FeatureName;

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.HttpAuthentication;

	/// <inheritdoc/>
	public IConfiguration CheckConfiguration(IConfiguration configuration)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		_authPolicy = configuration.GetConfiguration<AuthenticationFeatureOptions>(AuthenticationFeatureOptions.SectionName);
		Enabled = _authPolicy?.Enabled == true;
		return configuration;
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		if (!Enabled)
		{
			return services;
		}

		// Register config
		_ = services
			.ConfigureOptions<AuthenticationFeatureOptions>(configuration, AuthenticationFeatureOptions.SectionName)
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(o => JwtBearerEventsHandlers.OnConfigure(o, _authPolicy!));

		return
			services
				.AddHttpContextAccessor()
				.AddTransient<IImpersonationDataProvider, HttpImpersonationDataProvider>()
				.AddTransient<IUserAuthenticationTokenProvider, UserAuthenticationTokenProvider>()
				.AddTransient<IUserRefreshTokenProvider, UserRefreshTokenProvider>();
	}

	/// <inheritdoc/>
	public IApplicationBuilder ConfigurePipeline(IApplicationBuilder app, IHostEnvironment env)
	{
		ArgumentNullException.ThrowIfNull(app);

		if (!Enabled)
		{
			return app;
		}

		_ = app
			.UseAuthentication()
		  .UseCookiePolicy(new()
			{
				MinimumSameSitePolicy = _authPolicy?.CookiePolicy?.DisableSameSiteRestriction == true
					? SameSiteMode.None
					: SameSiteMode.Strict,
				HttpOnly = HttpOnlyPolicy.Always,
				Secure = _authPolicy?.AuthorityConnection?.AllowInSecureCommunication == true
					? CookieSecurePolicy.SameAsRequest
					: CookieSecurePolicy.Always
			});

		return _authPolicy?.AuthorityConnection?.AllowInSecureCommunication == true
			? app
			: app.UseHttpsRedirection();
	}
}
