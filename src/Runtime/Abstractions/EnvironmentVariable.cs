using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Runtime;

/// <summary>
/// Well known Environment variable names.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class EnvironmentVariable
{
	/// <summary>
	/// .NET Core environment mode.
	/// </summary>
	public const string DotNetEnvironmentMode = "DOTNET_ENVIRONMENT";

	/// <summary>
	/// ASP.NET Core environment mode.
	/// </summary>
	public const string AspNetCoreEnvironmentMode = "ASPNETCORE_ENVIRONMENT";

	/// <summary>
	/// ASP.NET Core version.
	/// </summary>
	public const string AspNetVersion = "ASPNET_VERSION";
}
