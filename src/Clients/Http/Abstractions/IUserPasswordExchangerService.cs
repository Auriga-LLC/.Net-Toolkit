namespace Toolkit.Extensions.Clients.Http;

public interface IUserPasswordExchangerService
{
	ValueTask<(string scheme, string jwtToken)> ExchangeUserPasswordAsync(string user, string password, CancellationToken cancellationToken = default);
}
