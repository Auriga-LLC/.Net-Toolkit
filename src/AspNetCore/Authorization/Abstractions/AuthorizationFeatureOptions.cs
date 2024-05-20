using System.Diagnostics.CodeAnalysis;
using Auriga.Toolkit.Configuration;

namespace Auriga.Toolkit.AspNetCore.Authorization;

/// <summary>
/// Authorization feature policy model.
/// </summary>
/// <example>
/// Usage in <c>appsettings.json</c>
/// <code>
/// {
///   "HTTP": {
///     "Authorization": {
///       "Enabled" : true
///     }
///   }
/// }
/// </code>
/// </example>
[ExcludeFromCodeCoverage]
public sealed class AuthorizationFeatureOptions : PolicyOptions
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public const string SectionName = $"{ConfigurationSectionNames.HttpApi}:Authorization";
}
