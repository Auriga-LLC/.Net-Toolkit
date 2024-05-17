using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Cookie usage policy configuration model.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class CookiePolicyOptions
{
	/// <summary>
	/// Gets is cookie <c>SameSite</c> policy should be forced?
	/// </summary>
	/// <remarks>Not recommended to enable in <c>Production</c>.</remarks>
	/// <value><c>false</c> by default.</value>
	public bool DisableSameSiteRestriction { get; init; }

	/// <summary>
	/// HTTP-only cookie path restrictions, used for authentication.
	/// </summary>
	public IReadOnlyDictionary<string, string>? RestrictionPaths { get; init; }
}
