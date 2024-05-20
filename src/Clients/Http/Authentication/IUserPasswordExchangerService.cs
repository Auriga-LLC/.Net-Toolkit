namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// User/password pair exchanger for token service contract.
/// </summary>
public interface IUserPasswordExchangerService
{
	ValueTask<(string scheme, string jwtToken)> ExchangeUserPasswordAsync(string user, string password, CancellationToken cancellationToken = default);
}
