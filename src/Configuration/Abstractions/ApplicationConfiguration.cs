using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Base application configuration model.
/// </summary>
[ExcludeFromCodeCoverage]
public class ApplicationConfiguration
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public const string SectionName = "Application";

	/// <summary>
	/// Gets application name.
	/// </summary>
	public string? Name { get; init; }
}
