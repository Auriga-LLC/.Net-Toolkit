using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Toolkit.Extensions.Authentication.OpenIdConnect;

/// <summary>
/// Auth provider response model.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed record OpenIdConnectTokenResponseModel
{
	/// <summary>
	/// Type of token which was returned.
	/// </summary>
	[JsonPropertyName("token_type")]
	[JsonProperty("token_type")]
	public required string TokenType { get; set; }

	/// <summary>
	/// Access token body.
	/// </summary>
	[JsonPropertyName("access_token")]
	[JsonProperty("access_token")]
	public required string AccessToken { get; set; }

	/// <summary>
	/// Expiration timeout for access token.
	/// </summary>
	[JsonPropertyName("expires_in")]
	[JsonProperty("expires_in")]
	public int AccessTokenExpiresIn { get; set; }

	/// <summary>
	/// Refresh token body.
	/// </summary>
	[JsonPropertyName("refresh_token")]
	[JsonProperty("refresh_token")]
	public required string RefreshToken { get; set; }

	/// <summary>
	/// Expiration timeout for refresh token.
	/// </summary>
	[JsonPropertyName("refresh_expires_in")]
	[JsonProperty("refresh_expires_in")]
	public int RefreshTokenExpiresIn { get; set; }

	/// <summary>
	/// Token session Id.
	/// </summary>
	[JsonPropertyName("session_state")]
	[JsonProperty("session_state")]
	public Guid SessionId { get; set; }

	/// <summary>
	/// Tokens requested scope.
	/// </summary>
	[JsonPropertyName("scope")]
	[JsonProperty("scope")]
	public required string RequestedScope { get; set; }

	/// <summary>
	/// A not-before policy ensures that any tokens issued before that time become invalid.
	/// Pushing a new not-before policy ensures that applications must download new public keys from project
	/// and mitigate damage from a compromised realm signing key.
	/// </summary>
	[JsonPropertyName("not-before-policy")]
	[JsonProperty("not-before-policy")]
	public long PolicyCreatedTimestamp { get; set; }
}
