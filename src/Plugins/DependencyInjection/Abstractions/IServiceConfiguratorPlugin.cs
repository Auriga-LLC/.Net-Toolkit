using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Services configurator plugin contract.
/// </summary>
public interface IServiceConfiguratorPlugin : IFeaturePlugin
{
	/// <summary>
	/// Setup plugin-specific services in the specified <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration);
}
