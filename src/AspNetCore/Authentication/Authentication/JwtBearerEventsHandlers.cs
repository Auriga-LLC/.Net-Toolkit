using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Auriga.Toolkit.Audit.Abstractions;
using Auriga.Toolkit.Authentication.OpenIdConnect;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// JWT token events handlers class.
/// </summary>
internal static class JwtBearerEventsHandlers
{
	public static void OnConfigure(JwtBearerOptions o, AuthenticationFeatureOptions authenticationOptions)
	{
		o.RequireHttpsMetadata = true;
		o.SaveToken = true;
		o.MetadataAddress = DefaultOpenIdConnectWellKnownMetadataUrlProvider.GetWellKnownMetadataUrl(
			authenticationOptions.AuthorityConnection.InternalEndpoint,
			authenticationOptions.AuthorityConnection.Realm);

		o.TokenValidationParameters = new TokenValidationParameters
		{
			// NOTE: Usually we don't need to set the issuer since the middleware will extract it
			// from the .well-known endpoint provided above, but since we are using the container name in
			// the above URL which is not what is published issueer by the well-known, we are setting it here.
			ValidateIssuerSigningKey = true,
			ValidIssuers = new []
			{
				authenticationOptions.AuthorityConnection.PublicAuthorityUrl.AbsoluteUri,
				authenticationOptions.AuthorityConnection.InternalAuthorityUrl.AbsoluteUri
			},
			ValidAudience = authenticationOptions.AuthorityConnection.Audience,
			ClockSkew = TimeSpan.FromMinutes(1)
		};
		// NOTE: In "Debug" mode we can allow to violate some auth policies
		if (!EnvironmentHelper.IsInProductionMode())
		{
			o.RequireHttpsMetadata = authenticationOptions.AuthorityConnection.AllowInSecureCommunication == false;
			o.TokenValidationParameters.ValidateLifetime = authenticationOptions.TokenPolicy?.AllowExpired == false;
		}

		o.Events = new JwtBearerEvents
		{
			OnTokenValidated = OnTokenValidatedCallback,
			OnMessageReceived = OnMessageReceivedCallback,
			OnAuthenticationFailed = OnAuthenticationFailedCallback
		};
	}

	/// <inheritdoc cref="JwtBearerEvents.OnTokenValidated"/>
	public static Task OnTokenValidatedCallback(TokenValidatedContext ctx)
	{
		ArgumentNullException.ThrowIfNull(ctx);

		if (ctx.SecurityToken is not JwtSecurityToken jwtToken)
		{
			return Task.CompletedTask;
		}

		string? userName = jwtToken.Claims
			.FirstOrDefault(x => string.Equals(x.Type, nameof(ClaimTypes.Upn), StringComparison.OrdinalIgnoreCase))?.Value;
		ctx.HttpContext.RequestServices.GetService<IAuditService>()?
			.AuditDebugMessage(
				AuditEventType.TokenValidated,
				$"Successfully authenticated users \"({userName}\" token");

		return Task.CompletedTask;
	}

	/// <inheritdoc cref="JwtBearerEvents.OnMessageReceived"/>
	public static Task OnMessageReceivedCallback(MessageReceivedContext ctx)
	{
		ArgumentNullException.ThrowIfNull(ctx);

		// Because token is splitted, we should concat it here
		IUserAuthenticationTokenProvider? authenticationTokenProvider = ctx.HttpContext.RequestServices.GetService<IUserAuthenticationTokenProvider>();
		if (authenticationTokenProvider == null)
		{
			ctx.Fail("No IUserAuthenticationTokenProvider");
			return Task.CompletedTask;
		}

		(string? _, string? authToken) = authenticationTokenProvider.GetAuthenticationToken(ctx.Request);
		if (string.IsNullOrWhiteSpace(authToken))
		{
			ctx.Fail("No authToken");
			return Task.CompletedTask;
		}

		ctx.Token = authToken;
		return Task.CompletedTask;
	}

	/// <inheritdoc cref="JwtBearerEvents.OnAuthenticationFailed"/>
	public static Task OnAuthenticationFailedCallback(AuthenticationFailedContext ctx)
	{
		ArgumentNullException.ThrowIfNull(ctx);

		switch (ctx.Exception)
		{
			case SecurityTokenExpiredException:
			{
				_ = ctx.Response.Headers.TryAdd(HeaderNames.AccessControlExposeHeaders, "WWW-Authenticate");
				ctx.HttpContext.RequestServices.GetService<IAuditService>()?
					.AuditWarningMessage(AuditEventType.TokenExpired, ctx.Exception.Message);
			} break;

			default:
			{
				ctx.HttpContext.RequestServices.GetService<IAuditService>()?
					.AuditWarningMessage(AuditEventType.TokenValidationFailed, ctx.Exception.Message);
			} break;
		}

		return Task.CompletedTask;
	}
}
