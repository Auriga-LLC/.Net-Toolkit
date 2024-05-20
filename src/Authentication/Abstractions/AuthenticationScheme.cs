using System.ComponentModel;

namespace Auriga.Toolkit.Authentication.Abstractions;

/// <summary>
/// Supported authentication schemes.
/// </summary>
public enum AuthenticationScheme
{
	/// <summary>
	/// Requests should use plain user/password pair.
	/// </summary>
	/// <remarks><see href="https://datatracker.ietf.org/doc/html/rfc7617">RFC 7617</see>.</remarks>
	[Description("Basic")]
	Basic,

	/// <summary>
	/// Requests should use bearer token.
	/// </summary>
	/// <remarks><see href="https://datatracker.ietf.org/doc/html/rfc6750">RFC 6750</see>.</remarks>
	[Description("Bearer")]
	Bearer
}
