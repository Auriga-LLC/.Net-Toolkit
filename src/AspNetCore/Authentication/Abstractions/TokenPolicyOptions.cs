namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Token policy configuration model.
/// </summary>
public sealed class TokenPolicyOptions
{
	/// <summary>
	/// Gets is expired auth tokens should be treated as valid?
	/// </summary>
	/// <remarks>Not recommended to enable in *Production*.</remarks>
	/// <value><c>false</c> by default.</value>
	public bool AllowExpired { get; init; }
}
