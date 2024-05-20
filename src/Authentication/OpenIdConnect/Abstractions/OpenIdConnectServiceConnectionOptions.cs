using System.ComponentModel;
using System.Diagnostics;
using Auriga.Toolkit.Authentication.Abstractions;
using Auriga.Toolkit.Clients.Http;
using Auriga.Toolkit.Configuration;

namespace Auriga.Toolkit.Authentication.OpenIdConnect;

/// <summary>
/// Identity connection configuration options.
/// </summary>
public sealed class OpenIdConnectServiceConnectionOptions : HttpConnectionOptions
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public new const string SectionName = $"{ConfigurationSectionNames.HttpApi}:Authentication:AuthorityConnection";

	/// <summary>
	/// Gets service internal endpoint URL.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public required Uri InternalEndpoint { get => Endpoint; init => Endpoint = value; }

	/// <summary>
	/// Gets authentication realm used.
	/// </summary>
	/// <value><c>string.Empty</c> is default.</value>
	public string Realm { get; init; } = string.Empty;

	/// <summary>
	/// Gets identity service type.
	/// </summary>
	public IdentityServiceType ServiceType { get; init; }

	/// <summary>
	/// Gets requested response type.
	/// </summary>
	/// <remarks>According to implemented flow type <see href="https://tools.ietf.org/html/rfc6749#section-4.1"/>.</remarks>
	/// <value><c>string.Empty</c> is default.</value>
	public string RequestedResponseType { get; init; } = string.Empty;
}
