using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Auriga.Toolkit.Plugins;

/// <summary>
/// Basic plugin.
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class FeaturePlugin : IFeaturePlugin
{
	///<inheritdoc/>
	public abstract string Name { get; }

	///<inheritdoc/>
	public abstract int LoadOrder { get; }

	///<inheritdoc/>
	public bool Enabled { get; set; } = true;

	///<inheritdoc/>
	public ILogger Logger { get; set; } = default!;
}
