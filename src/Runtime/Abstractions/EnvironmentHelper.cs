using Microsoft.Extensions.Hosting;

namespace Auriga.Toolkit.Runtime.Abstractions;

/// <summary>
/// Environment related extensions.
/// </summary>
public static class EnvironmentHelper
{
	/// <summary>
	/// Checks is running in production mode.
	/// </summary>
	/// <returns><c>true</c> if app running in production mode, otherwise <c>false</c>.</returns>
	public static bool IsInProductionMode()
		=> string.Equals(GetEnvironmentMode(), Environments.Production, StringComparison.Ordinal);

	/// <summary>
	/// Checks is running in ASP.NET Core environment.
	/// </summary>
	/// <returns><c>true</c> if app running as ASP.NET Core, otherwise <c>false</c>.</returns>
	public static bool IsAspNetCoreApp() =>
		!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(EnvironmentVariable.AspNetCoreEnvironmentMode))
		|| !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(EnvironmentVariable.AspNetVersion));

	/// <summary>
	/// Gets environmnet mode variable value.
	/// </summary>
	/// <returns>Environmnet mode name.</returns>
	/// <exception cref="InvalidOperationException">If not environment variable set.</exception>
	public static string GetEnvironmentMode()
	{
		string? env = IsAspNetCoreApp()
			? Environment.GetEnvironmentVariable(EnvironmentVariable.AspNetCoreEnvironmentMode)
			: Environment.GetEnvironmentVariable(EnvironmentVariable.DotNetEnvironmentMode);
		if (string.IsNullOrWhiteSpace(env))
		{
			throw new InvalidOperationException("*_ENVIRONMENT variable not set.");
		}

		return env;
	}
}
