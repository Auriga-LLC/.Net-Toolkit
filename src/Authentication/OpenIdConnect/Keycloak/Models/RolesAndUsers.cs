using System.Diagnostics.CodeAnalysis;

namespace Toolkit.Extensions.Authentication.OpenIdConnect.Keycloak.Models;

[ExcludeFromCodeCoverage]
public sealed class RolesAndUsers
{
	public IList<RolesWithUsersModel>? Roles { get; init; }

	public IList<UserModel>? Users { get; init; }
}
