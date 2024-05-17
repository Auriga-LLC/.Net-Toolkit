namespace Toolkit.Extensions.Clients.Http;

public interface IOfflineTokenExchangerService
{
	ValueTask<(string scheme, string jwtToken)> ExchangeOfflineTokenAsync(string offlineToken, CancellationToken cancellationToken = default);
}
