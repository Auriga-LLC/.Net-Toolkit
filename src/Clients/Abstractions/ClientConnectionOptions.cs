using System.Diagnostics.CodeAnalysis;
using Toolkit.Extensions.Configuration;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Base configuration model for remote service connection.
/// </summary>
/// <remarks>Not intended to be used directly.</remarks>
[ExcludeFromCodeCoverage]
public abstract class ClientConnectionOptions
{
	/// <summary>
	/// Section name in <c>appsettings.json</c> file.
	/// </summary>
	public const string SectionName = $"{ConfigurationSectionName.BaseSection}:Integration";

	/// <summary>
	/// Gets service endpoint URL.
	/// </summary>
	/// <value><c>null</c> by default.</value>
	public Uri? Endpoint { get; init; }

	/// <summary>
	/// Gets operations timeout.
	/// </summary>
	/// <value>Defaults to *60* seconds.</value>
	public TimeSpan TimeOut { get; init; } = TimeSpan.FromSeconds(60);

	/// <summary>
	/// Gets interval between keep-alive messages being sent.
	/// </summary>
	/// <value>*15 seconds* by default.</value>
	public TimeSpan KeepAlive { get; init; } = TimeSpan.FromSeconds(15);

	/// <summary>
	/// Gets connection <see cref="ClientAuthenticationOptions">authentication policy</see>.
	/// </summary>
	public ClientAuthenticationOptions? Authentication { get; init; }

	/// <summary>
	/// Gets configured <see cref="ResponseCachingOptions"> requests caching policy</see>.
	/// </summary>
	public ResponseCachingOptions? ResponseCaching { get; init; } = new();

	/// <summary>
	/// Gets configured <see cref="RequestTracingOptions"> requests diagnostic policy</see>.
	/// </summary>
	public RequestTracingOptions RequestTracing { get; init; } = new();

	/// <summary>
	/// Gets connection <see cref="ConnectionRetryOptions">retry policy</see>.
	/// </summary>
	public ConnectionRetryOptions Resilience { get; init; } = new();
}
