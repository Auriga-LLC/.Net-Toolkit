using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Auriga.Toolkit.Authentication.OpenIdConnect;

public interface IOpenIdConnectConfigurationProvider
{
	ValueTask<OpenIdConnectConfiguration> GetConfigurationAsync(Uri? endpoint, string? realmName = null, CancellationToken cancellationToken = default);
}
