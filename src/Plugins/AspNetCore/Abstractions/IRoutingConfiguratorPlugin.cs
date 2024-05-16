using Microsoft.AspNetCore.Routing;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Routing configurator plugin contract.
/// </summary>
public interface IRoutingConfiguratorPlugin : IFeaturePlugin
{
	/// <summary>
	/// Setup plugin-specific routing in the specified <see cref="IEndpointRouteBuilder"/>.
	/// </summary>
	/// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to build up application routing.</param>
	/// <returns>The <see cref="IEndpointRouteBuilder"/> so that additional calls can be chained.</returns>
	IEndpointRouteBuilder ConfigureRouting(IEndpointRouteBuilder endpoints);
}
