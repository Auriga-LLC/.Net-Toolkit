using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Prepare operation executor plugin contract.
/// </summary>
public interface IPrepareOperationExecutorPlugin : IFeaturePlugin
{
	/// <summary>
	/// Execute prepare operation on host builder.
	/// </summary>
	/// <param name="hostBuilder">The <see cref="IHostBuilder"/> to execute operations.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <param name="loggingBuilder">The <see cref="ILoggingBuilder"/> to execute operations.</param>
	/// <returns>The <see cref="IHostBuilder"/> so that additional calls can be chained.</returns>
	IHostBuilder ExecutePrepareOperation(IHostBuilder hostBuilder, IConfiguration configuration, ILoggingBuilder loggingBuilder);
}
