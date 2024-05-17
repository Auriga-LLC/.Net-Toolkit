using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Toolkit.Extensions.Authentication.OpenIdConnect;

public interface IOpenIdConnectConfigurationProvider
{
	ValueTask<OpenIdConnectConfiguration> GetConfigurationAsync(Uri? endpoint, string? realmName = null, CancellationToken cancellationToken = default);
}
