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

		IConfigurationSection currentSection = configuration.GetSection(AuthenticationFeatureOptions.SectionName);
		if (!currentSection.Exists())
		{
			Enabled = false;
		}

		return configuration;
	}

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		// Register config
		_authPolicy = services.RegisterConfigurationIfExists<AuthenticationFeatureOptions>(configuration, AuthenticationFeatureOptions.SectionName);
		Enabled = _authPolicy?.Enabled == true;
		if (!Enabled)
		{
			return services;
		}

		_ = services.AddAuthentication(options =>
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

		_ = app.UseAuthentication();
		var cookiePolicy = new Microsoft.AspNetCore.Builder.CookiePolicyOptions
		{
			MinimumSameSitePolicy = _authPolicy?.CookiePolicy?.DisableSameSiteRestriction == true
				? SameSiteMode.None
				: SameSiteMode.Strict,
			HttpOnly = HttpOnlyPolicy.Always,
			Secure = _authPolicy?.Connection?.AllowInSecureCommunication == true
				? CookieSecurePolicy.SameAsRequest
				: CookieSecurePolicy.Always
		};
		_ = app.UseCookiePolicy(cookiePolicy);

		return _authPolicy?.Connection?.AllowInSecureCommunication == true
			? app
			: app.UseHttpsRedirection();
	}
}
