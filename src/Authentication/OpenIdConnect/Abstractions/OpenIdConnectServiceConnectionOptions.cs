using Toolkit.Extensions.Authentication.Abstractions;
using Toolkit.Extensions.Clients.Http;

namespace Toolkit.Extensions.Authentication.OpenIdConnect;

/// <summary>
/// Identity connection configuration options.
/// </summary>
public sealed class OpenIdConnectServiceConnectionOptions : ClientConnectionOptions
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public new const string SectionName = $"{ClientConnectionOptions.SectionName}:IdentityService";

	/// <summary>
	/// Gets authentication realm used.
	/// </summary>
	/// <value><c>string.Empty</c> is default.</value>
	public string Realm { get; init; } = string.Empty;

	public IdentityServiceType ServiceType { get; init; }

	/// <summary>
	/// Gets requested response type.
	/// </summary>
	/// <remarks>According to implemented flow type <see href="https://tools.ietf.org/html/rfc6749#section-4.1"/>.</remarks>
	/// <value><c>string.Empty</c> is default.</value>
	public string RequestedResponseType { get; init; } = string.Empty;
}
