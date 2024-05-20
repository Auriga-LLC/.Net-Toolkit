using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Configuration section names.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ConfigurationSectionNames
{
	/// <summary>
	/// Http API section name in <c>appsettings.json</c> file.
	/// </summary>
	public const string HttpApi = "HTTP";
	
	/// <summary>
	/// GraphQL API section name in <c>appsettings.json</c> file.
	/// </summary>
	public const string GraphQlApi = "GraphQL";
}
