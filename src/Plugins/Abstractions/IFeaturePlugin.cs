using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Feature plugin contract.
/// </summary>
public interface IFeaturePlugin
{
	/// <summary>
	/// Gets plugin name.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// Gets plugin load order.
	/// </summary>
	public int LoadOrder { get; }

	/// <summary>
	/// Gets or sets is plugin loading and execution enabled.
	/// </summary>
	public bool Enabled { get; set; }

	/// <summary>
	/// Logger service instance.
	/// </summary>
	public ILogger Logger { get; set; }
}
