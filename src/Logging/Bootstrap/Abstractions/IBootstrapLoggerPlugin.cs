using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Logging;

/// <summary>
/// Bootstrap logger setup plugin contract.
/// </summary>
public interface IBootstrapLoggerPlugin : IDisposable
{
	/// <summary>
	/// Gets bootstrap logger.
	/// </summary>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <returns>Logger builder delegate.</returns>
	void Init(IConfiguration configuration);

	/// <summary>
	/// Gets bootstrap logger.
	/// </summary>
	/// <param name="categoryName">Logger category name.</param>
	/// <returns>Logger builder delegate.</returns>
	ILogger GetBootstrapLogger(string categoryName);
}
