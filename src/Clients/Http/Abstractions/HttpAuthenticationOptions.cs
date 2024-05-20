using System.Diagnostics.CodeAnalysis;
using Auriga.Toolkit.Authentication.Abstractions;

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
///       "Authentication": {
///         "UserId": "testUserId",
///         "UserSecret": "testUserPassword"",
///         "Type": "Token",
///         "Scheme": "Bearer",
///         "Mode": "UserPassword"
///       }
///     }
///   }
/// }
/// </code>
/// </example>
[ExcludeFromCodeCoverage]
public sealed class HttpAuthenticationOptions : ClientAuthenticationOptions
{
	/// <summary>
	/// Gets remote request authentication scheme.
	/// </summary>
	/// <value><see cref="AuthenticationScheme.Bearer"/> by default.</value>
	/// <remarks>
	/// If <see cref="AuthenticationScheme.Bearer"/> scheme selected,<br/>
	/// <see cref="Mode"/> is used to determine token acquire mode.
	/// </remarks>
	public AuthenticationScheme Scheme { get; init; } = AuthenticationScheme.Bearer;
	
	/// <summary>
	/// Gets remote request authentication mode.
	/// </summary>
	/// <value><see cref="HttpTokenExchangeMode.UserPassword"/> by default.</value>
	/// <remarks>
	/// If <see cref="HttpTokenExchangeMode.Impersonation"/> mode selected,<br/>
	/// <see cref="ClientAuthenticationOptions.UserId"/> and <see cref="ClientAuthenticationOptions.UserSecret"/> is ignored.
	/// </remarks>
	public HttpTokenExchangeMode Mode { get; init; } = HttpTokenExchangeMode.UserPassword;
}
