using Microsoft.Extensions.Configuration;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Configuration checker plugin contract.
/// </summary>
public interface IConfigurationCheckerPlugin : IFeaturePlugin
{
	/// <summary>
	/// Checks plugin-specific settings in the specified <see cref="IConfiguration"/>.
	/// </summary>
	/// <remarks>Intended to be used as a place for checking configuration values and etc.</remarks>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <returns>The <see cref="IConfiguration"/> so that additional calls can be chained.</returns>
	IConfiguration CheckConfiguration(IConfiguration configuration);
}
