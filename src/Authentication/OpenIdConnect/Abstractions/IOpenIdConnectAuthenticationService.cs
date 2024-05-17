using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Authentication.OpenIdConnect;

public interface IOpenIdConnectAuthenticationService
{
	ValueTask<OperationContext<string>> GetLoginPageUrlAsync(Uri redirectUri, string state, CancellationToken cancellationToken = default);

	ValueTask<OperationContext<OpenIdConnectTokenResponseModel?>> ExchangeCodeForTokenAsync(Uri redirectUri, string code, CancellationToken cancellationToken = default);

	ValueTask<OperationContext<OpenIdConnectTokenResponseModel?>> ExchangeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

	ValueTask<OperationContext<OpenIdConnectTokenResponseModel?>> ExchangeUserPasswordForTokenAsync(string username, string password, CancellationToken cancellationToken = default);

	ValueTask<OperationContext<OpenIdConnectTokenResponseModel?>> RequestTokenAsync(
		string grantType,
		string? username = null,
		string? password = null,
		string? refreshToken = null,
		Uri? redirectUri = null,
		string? code = null,
		CancellationToken cancellationToken = default);

	ValueTask<OperationContext> LogoutAsync(
		string refreshToken,
		Uri redirectUri,
		string state,
		CancellationToken cancellationToken = default);

	ValueTask<OperationContext> RevokeClientSessionAsync(string refreshToken, CancellationToken cancellationToken = default);
}
