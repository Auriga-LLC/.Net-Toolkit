using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Authentication provider connection configuration model.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class AuthenticationProviderConnectionOptions
{
	/// <summary>
	/// Gets is communication with authority can be insecure (without https)?
	/// </summary>
	/// <remarks>Not recommended to enable in *Production*.</remarks>
	/// <value><c>false</c> by default.</value>
	public bool AllowInSecureCommunication { get; init; }

	/// <summary>
	/// Gets public authority URI.
	/// </summary>
	/// <remarks>Should be like <c>{Uri}/auth/realms/{Realm}</c>.</remarks>
	public Uri? Authority { get; init; }

	/// <summary>
	/// Gets authority audience.
	/// </summary>
	/// <value><c>string.Empty</c> is default.</value>
	public string Audience { get; init; } = string.Empty;
}
