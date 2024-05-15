using Microsoft.Extensions.Hosting;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Operation executor plugin contract.
/// </summary>
public interface IPostOperationExecutorPlugin : IFeaturePlugin
{
	/// <summary>
	/// Executes plugin-specific operations in the specified <see cref="IHost"/>.
	/// </summary>
	/// <remarks>E.g. here we can apply EF migrations.</remarks>
	/// <param name="app">The <see cref="IHost"/> to execute operations.</param>
	/// <returns>The <see cref="IHost"/> so that additional calls can be chained.</returns>
	IHost ExecutePostOperation(IHost app);
}
