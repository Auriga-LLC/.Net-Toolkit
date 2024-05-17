using System.Diagnostics.CodeAnalysis;
using Auriga.Toolkit.Configuration;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Authentication feature model.
/// </summary>
/// <remarks>
/// Recommended to enable this feature, otherwise you should handle users authentication by yourself.<br/>
/// Automatically registers required authentication functionality.
/// </remarks>
/// <example>
/// Usage in "appsettings.json*:
/// <code>
/// "AppSettings": {
/// 	"HTTP": {
/// 		...
/// 		"Authentication": {
/// 			"Enabled" : true,
///			},
///			...
///		}
/// }
/// </code>
/// </example>
[ExcludeFromCodeCoverage]
public sealed class AuthenticationFeatureOptions : PolicyOptions
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public const string SectionName = $"{ApplicationApiConfiguration.SectionName}:Authentication";

	/// <summary>
	/// Gets connection configuration.
	/// </summary>
	public AuthenticationProviderConnectionOptions? Connection { get; init; }

	/// <summary>
	/// Gets cookie policy configuration.
	/// </summary>
	public CookiePolicyOptions? CookiePolicy { get; init; }

	/// <summary>
	/// Gets token policy configuration.
	/// </summary>
	public TokenPolicyOptions? TokenPolicy { get; init; }
}
