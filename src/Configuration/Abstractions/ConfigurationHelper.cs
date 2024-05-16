using Auriga.Toolkit.Runtime.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Auriga.Toolkit.Configuration.Abstractions;

/// <summary>
/// Configuration helper class.
/// </summary>
public static class ConfigurationHelper
{
	/// <summary>
	/// Bootstraps configuration from <c>appsettings.json</c> in environment dependent manner.
	/// </summary>
	/// <returns><see cref="IConfigurationRoot"/> to be used as configuration source.</returns>
	public static IConfigurationRoot BootstrapConfiguration()
		=> new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
			.AddJsonFile($"appsettings.{EnvironmentHelper.GetEnvironmentMode()}.json", optional: true, reloadOnChange: false)
			.Build();
}
