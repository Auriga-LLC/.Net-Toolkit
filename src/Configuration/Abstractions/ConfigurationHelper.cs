using Microsoft.Extensions.Configuration;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Configuration helper class.
/// </summary>
public static class ConfigurationHelper
{
	/// <summary>
	/// Bootstraps configuration from <c>appsettings.json</c> in Environment dependent manner.
	/// </summary>
	/// <returns><see cref="IConfigurationRoot"/> to be used as configuration source.</returns>
	public static IConfigurationRoot BootstrapConfiguration()
		=> new ConfigurationBuilder()
			.AddCommandLine(Environment.GetCommandLineArgs())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
			.AddJsonFile($"appsettings.{EnvironmentHelper.GetEnvironmentMode()}.json", optional: true, reloadOnChange: false)
			.AddEnvironmentVariables()
			.Build();
}
