using System.Diagnostics.CodeAnalysis;

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
/// "AppSettings": {
///   "DiagnosticPolicy": (
///     "Enabled": true,
///     "TraceHttp": true
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
	public const string SectionName = $"{"ApplicationApiConfiguration.SectionName"}:DiagnosticPolicy";

	/// <summary>
	/// Gets is all HTTP requests/responses should be traced.
	/// </summary>
	public bool TraceHttp { get; init; }
}
