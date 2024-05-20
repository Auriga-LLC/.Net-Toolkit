using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Auriga.Toolkit.Authentication.OpenIdConnect.Keycloak.Models;

/// <summary>
/// Auth provider role model.
/// </summary>
[ExcludeFromCodeCoverage]
public record RoleModel
{
	/// <summary>
	/// Gets or sets role Id.
	/// </summary>
	[JsonProperty("id")]
	public Guid Id { get; set; }

	/// <summary>
	/// Gets or sets role name.
	/// </summary>
	[JsonProperty("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets role description.
	/// </summary>
	[JsonProperty("description")]
	public string Description { get; set; } = string.Empty;
}
