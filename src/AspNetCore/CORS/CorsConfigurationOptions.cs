using System.Diagnostics.CodeAnalysis;
using Auriga.Toolkit.Configuration.Abstractions.Configuration;
using Auriga.Toolkit.Configuration.Abstractions.Models;

namespace Auriga.Toolkit.AspNetCore.CORS;

/// <summary>
/// HTTP CORS feature policy model.
/// </summary>
/// <remarks>
/// Recommended to enable this feature, otherwise you should handle and implement CORS handling by yourself.<br/>
/// Automatically registers required CORS functionality.<br/>
/// Configured settings will be used under reserved policy '"MyCorsPolicy"'.
/// </remarks>
/// <example>
/// Usage in `appsettings.json`:
/// <code>
/// "AppSettings": {
///   "HTTP":
///     "CORS": {
///       "Enabled": false,
///       ...
///     }
///   }
/// }
/// </code>
/// </example>
[ExcludeFromCodeCoverage]
public sealed class CorsFeatureOptions : BasicPolicyModel
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public const string SectionName = $"{ApplicationApiConfiguration.SectionName}:CORS";

	/// <summary>
	/// CORS policies.
	/// </summary>
	public IReadOnlyDictionary<string, CorsPolicyOptions>? Policies { get; init; }
}
