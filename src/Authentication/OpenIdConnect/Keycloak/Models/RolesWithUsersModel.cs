using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Authentication.OpenIdConnect.Keycloak.Models;

/// <summary>
/// Role Model with Users.
/// </summary>
[ExcludeFromCodeCoverage]
public record RolesWithUsersModel : RoleModel
{
	/// <summary>
	/// Users with this role.
	/// </summary>
	public IList<UserModel>? Users { get; init; }
}
