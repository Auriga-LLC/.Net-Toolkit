using Auriga.Toolkit.Authentication.OpenIdConnect.Keycloak.Models;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Authentication.OpenIdConnect;

public interface IKeycloakRolesServiceClient {
	Task<OperationContext<RolesAndUsers?>> GetRolesAndUsersAsync(CancellationToken cancellationToken = default);
}

/*/ <inheritdoc/>
public async Task<OperationResult<RolesAndUsers?>> GetRolesAndUsersAsync(
	CancellationToken cancellationToken = default)
{
	return null;
	/*
	using var requestMessage = new HttpRequestMessage(
		HttpMethod.Get,
		urlProvider.GetOperationUrl(KeycloakApiOperationType.GetUsersAndRoles));

	return await ExecuteRequestAsync<RolesAndUsers>(
		requestMessage,
		cancellationToken
	).ConfigureAwait(false);
}*/
