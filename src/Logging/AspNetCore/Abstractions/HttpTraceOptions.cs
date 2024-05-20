using System.Diagnostics.CodeAnalysis;
using Auriga.Toolkit.Configuration;

namespace Auriga.Toolkit.Logging;

/// <summary>
/// Self-diagnostic feature policy model.
/// </summary>
/// <remarks>
/// Recommended to enable this feature, otherwise you should handle and implement self-diagnostic by yourself.
/// Automatically registers required diagnostic functionality.
/// </remarks>
/// <example>
/// Usage in <c>appsettings.json</c>:
/// <code>
/// {
///   "Http": {
///     "DiagnosticPolicy": (
///       "TraceHttp": true
///     }
///   }
/// }
/// </code>
/// </example>
[ExcludeFromCodeCoverage]
public sealed class HttpTraceOptions
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public const string SectionName = $"{ConfigurationSectionNames.HttpApi}:DiagnosticPolicy";

	/// <summary>
	/// Gets is all HTTP requests/responses should be traced.
	/// </summary>
	public bool TraceHttp { get; init; }
}
