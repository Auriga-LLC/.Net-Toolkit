using Keycloak.Plugin.Abstractions.Models;
using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Authentication.OpenIdConnect;

public interface IKeycloakUsersServiceClient
{
	ValueTask<OperationContext<UserInfoResponseModel?>> GetUserInfoAsync(string autheticationData, CancellationToken cancellationToken = default);

/*

    public async Task<UserInfoResponseModel> GetUserInfoAsync(string token)
    {
        var userInfoUrl = $"{_config.ClientBaseUrl}realms/{_config.ClientRealmName}/protocol/openid-connect/userinfo";
        using var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, userInfoUrl);
        userInfoRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var result = await HttpClient.SendAsync(userInfoRequest);
        var s2 = await result.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<UserInfoResponseModel>(s2)!;
    }
*/
}
