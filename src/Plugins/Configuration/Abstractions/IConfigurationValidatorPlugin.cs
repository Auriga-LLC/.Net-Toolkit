using Microsoft.Extensions.Configuration;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Configuration validator plugin contract.
/// </summary>
public interface IConfigurationValidatorPlugin : IFeaturePlugin
{
	/// <summary>
	/// Validates plugin-specific settings in the specified <see cref="IConfiguration"/>.
	/// </summary>
	/// <remarks>Intended to be used as a place for checking old configuration values and etc.</remarks>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <returns>The <see cref="IConfiguration"/> so that additional calls can be chained.</returns>
	IConfiguration ValidateConfiguration(IConfiguration configuration);
}
