using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Basic policy model.
/// </summary>
/// <remarks>Not intended to be directly used.</remarks>
[ExcludeFromCodeCoverage]
public abstract class PolicyOptions
{
	/// <summary>
	/// Gets is related functionality enabled.
	/// </summary>
	/// <value><c>false</c> by default.</value>
	public bool Enabled { get; init; }
}
