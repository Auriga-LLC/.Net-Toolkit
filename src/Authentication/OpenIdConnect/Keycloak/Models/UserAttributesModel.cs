using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Auriga.Toolkit.Authentication.OpenIdConnect.Keycloak.Models;

/// <summary>
/// Custom attributes model.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed record UserAttributesModel
{
	/// <summary>
	/// Gets or sets users mobile.
	/// </summary>
	[JsonProperty("mobile")]
	public IList<string>? Mobile { get; init; }
}
