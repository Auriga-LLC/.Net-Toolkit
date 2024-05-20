namespace Auriga.Toolkit.Clients.Http;

public interface IOfflineTokenExchangerService
{
	ValueTask<(string scheme, string jwtToken)> ExchangeOfflineTokenAsync(string offlineToken, CancellationToken cancellationToken = default);
}
