using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Toolkit.Extensions.Authentication.OpenIdConnect.Keycloak.Models;

/// <summary>
/// Auth provider user model.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed record UserModel
{
	/// <summary>
	/// Gets or sets user Id.
	/// </summary>
	[JsonProperty("id")]
	public Guid Id { get; init; }

	/// <summary>
	/// Gets or sets user name.
	/// </summary>
	[JsonProperty("username")]
	public string? UserName { get; init; }

	/// <summary>
	/// Gets or sets user full name.
	/// </summary>
	[JsonProperty("fullname")]
	public string? FullName { get; init; }

	/// <summary>
	/// Gets or sets users first name.
	/// </summary>
	[JsonProperty("firstName")]
	public string? FirstName { get; init; }

	/// <summary>
	/// Gets or sets users last name.
	/// </summary>
	[JsonProperty("lastName")]
	public string? LastName { get; init; }

	/// <summary>
	/// Gets or sets users email.
	/// </summary>
	[JsonProperty("email")]
	public string? Email { get; init; }

	/// <summary>
	/// Gets or sets users mobile phone.
	/// </summary>
	[JsonProperty("mobile")]
	public string? MobilePhone { get; init; }

	/// <summary>
	/// Gets or sets users attributes.
	/// </summary>
	[JsonProperty("attributes")]
	public UserAttributesModel? Attributes { get; init; }
}
