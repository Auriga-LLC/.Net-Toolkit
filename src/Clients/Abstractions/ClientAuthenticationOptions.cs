using System.Diagnostics.CodeAnalysis;
using Auriga.Toolkit.Configuration;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// "Log in to" remote service feature policy model.
/// </summary>
/// <remarks>Feature should be used together with another configuration model, you should implement it by yourself when used in new model.</remarks>
/// <example>
/// Usage in <c>appsettings.json</c>:
/// <code>
/// {
///   "Integration": {
///     "SomeRemoteService": {
///        ...
///       "Authentication": {
///         "UserId": "testUserId",
///         "UserSecret": "testUserPassword"",
///         "Mode": "UserPassword"
///       }
///     }
///   }
/// }
/// </code>
/// </example>
[ExcludeFromCodeCoverage]
public class ClientAuthenticationOptions : PolicyOptions
{
	/// <inheritdoc cref="PolicyOptions.Enabled"/>
	public new bool Enabled => Type != ClientAuthenticationType.Anonymous;

	/// <summary>
	/// Gets users name.
	/// </summary>
	/// <value><c>Empty</c> by default.</value>
	public string UserId { get; init; } = string.Empty;

	/// <summary>
	/// Gets users secret.
	/// </summary>
	/// <value><c>Empty</c> by default.</value>
	/// <remarks>E.g. its a password/secret/token issued by `keycloak` for domain service.</remarks>
	public string UserSecret { get; init; } = string.Empty;

	/// <summary>
	/// Gets remote request authentication mode.
	/// </summary>
	/// <value><see cref="ClientAuthenticationType.Anonymous"/> by default.</value>
	public ClientAuthenticationType Type { get; init; } = ClientAuthenticationType.Anonymous;
}
