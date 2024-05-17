using System.Net.Http.Headers;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// LogIn service for use with user/password pair.
/// </summary>
/// <remarks>Exchanges user/password pair for actual token.</remarks>
/// <param name="userPasswordExchanger">Password token exchanger service.</param>
internal sealed class UserPasswordLoginService(IUserPasswordExchangerService userPasswordExchanger) : IHttpServiceClientAuthenticationDataProvider
{
	/// <inheritdoc/>
	public async Task<AuthenticationHeaderValue> GetAuthenticationDataAsync(ClientAuthenticationOptions configuration, CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(configuration);

		(string scheme, string jwtToken) = await userPasswordExchanger.ExchangeUserPasswordAsync(configuration.UserId, configuration.UserSecret, cancellationToken)
			.ConfigureAwait(false);

		return new AuthenticationHeaderValue(scheme, jwtToken);
	}
}
