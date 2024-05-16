using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Bootstrap logger setup plugin contract.
/// </summary>
public interface IBootstrapLoggerPlugin
{
	/// <summary>
	/// Gets bootstrap logger.
	/// </summary>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <returns>Logger builder delegate.</returns>
	Func<string, ILogger> GetBootstrapLogger(IConfiguration configuration);
}
