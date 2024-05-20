using System.Net.Http.Headers;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// LogIn service for use with offline token.
/// </summary>
/// <remarks>Exchanges offline token for actual one.</remarks>
/// <param name="offlineTokenExchanger">Offline token exchanger service.</param>
internal sealed class OfflineTokenLoginService(IOfflineTokenExchangerService offlineTokenExchanger) : IHttpServiceClientAuthenticationDataProvider
{
	/// <inheritdoc/>
	public async Task<AuthenticationHeaderValue> GetAuthenticationDataAsync(ClientAuthenticationOptions configuration, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		(string scheme, string jwtToken) = await offlineTokenExchanger.ExchangeOfflineTokenAsync(configuration.UserSecret, cancellationToken)
			.ConfigureAwait(false);

		return new AuthenticationHeaderValue(scheme, jwtToken);
	}
}
