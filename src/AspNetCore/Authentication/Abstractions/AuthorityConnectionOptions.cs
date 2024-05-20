using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Authentication provider connection configuration model.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class AuthorityConnectionOptions
{
	private static readonly CompositeFormat s_authorityUrlFormat = CompositeFormat.Parse("{0}realms/{1}");

	/// <summary>
	/// Gets is communication with authority can be insecure (without https)?
	/// </summary>
	/// <remarks>Not recommended to enable in *Production*.</remarks>
	/// <value><c>false</c> by default.</value>
	public bool AllowInSecureCommunication { get; init; }

	private readonly Uri? _publicEndpoint;
	/// <summary>
	/// Gets service public endpoint URL.
	/// </summary>
	public required Uri PublicEndpoint
	{
		get => _publicEndpoint!;
		init => _publicEndpoint = new(value.GetNormalizedAbsoluteUri());
	}

	private readonly Uri? _internalEndpoint;
	/// <summary>
	/// Gets service internal endpoint URL.
	/// </summary>
	public Uri InternalEndpoint
	{
		get => _internalEndpoint ?? PublicEndpoint;
		init => _internalEndpoint = new(value.GetNormalizedAbsoluteUri());
	}

	/// <summary>
	/// Gets authentication realm used.
	/// </summary>
	public required string Realm { get; init; }

	private Uri? _publicAuthorityUrl;
	/// <summary>
	/// Gets public authority URI.
	/// </summary>
	/// <remarks>Should be like <c>{Uri}/realms/{Realm}</c>.</remarks>
	public Uri PublicAuthorityUrl
	{
		get
		{
			if (_publicAuthorityUrl == null)
			{
				_publicAuthorityUrl = new(string.Format(CultureInfo.InvariantCulture, s_authorityUrlFormat, PublicEndpoint, Realm));
			}

			return _publicAuthorityUrl;
		}
	}

	private Uri? _internalAuthorityUrl;
	/// <summary>
	/// Gets internal authority URI.
	/// </summary>
	/// <remarks>Should be like <c>{Uri}/realms/{Realm}</c>.</remarks>
	public Uri InternalAuthorityUrl
	{
		get
		{
			if (_internalAuthorityUrl == null)
			{
				_internalAuthorityUrl = new(string.Format(CultureInfo.InvariantCulture, s_authorityUrlFormat, InternalEndpoint, Realm));
			}

			return _internalAuthorityUrl;
		}
	}

	/// <summary>
	/// Gets authority audience.
	/// </summary>
	/// <value><c>string.Empty</c> is default.</value>
	public required string Audience { get; init; }
}
