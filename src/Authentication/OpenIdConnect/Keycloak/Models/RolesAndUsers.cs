using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Authentication.OpenIdConnect.Keycloak.Models;

[ExcludeFromCodeCoverage]
public sealed class RolesAndUsers
{
	public IList<RolesWithUsersModel>? Roles { get; init; }

	public IList<UserModel>? Users { get; init; }
}
