using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// Base configuration model for Http service connection.
/// </summary>
/// <remarks>Not intended to be used directly.</remarks>
[ExcludeFromCodeCoverage]
public abstract class HttpConnectionOptions : ClientConnectionOptions
{
	/// <summary>
	/// Gets connection <see cref="HttpAuthenticationOptions">authentication policy</see>.
	/// </summary>
	public new HttpAuthenticationOptions? Authentication { get; init; }

	/// <summary>
	/// Gets configured <see cref="ResponseCachingOptions"> requests caching policy</see>.
	/// </summary>
	public ResponseCachingOptions? ResponseCaching { get; init; } = new();
}
