using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Keycloak.Plugin.Abstractions.Models;

public class UserInfoResponseModel
{
	[JsonPropertyName("sub")]
	public Guid Id { get; set; }

	[JsonPropertyName("upn")]
	public string Username { get; set; } = string.Empty;

	[JsonPropertyName("email")]
	public string Email { get; set; } = string.Empty;

	[JsonPropertyName("preferred_username")]
	public string PreferredUsername { get; set; } = string.Empty;

	[JsonPropertyName("given_name")]
	public string FirstName { get; set; } = string.Empty;

	[JsonPropertyName("family_name")]
	public string LastName { get; set; } = string.Empty;

	[JsonPropertyName("name")]
	public string FullName { get; set; } = string.Empty;

	[JsonPropertyName("groups")]
	public ReadOnlyCollection<string> Groups { get; set; }

	[JsonPropertyName("email_verified")]
	public bool EmailVerified { get; set; }
}
