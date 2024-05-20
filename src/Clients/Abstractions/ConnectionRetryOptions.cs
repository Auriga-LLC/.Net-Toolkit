using System.Diagnostics.CodeAnalysis;
using Auriga.Toolkit.Configuration;

namespace Auriga.Toolkit.Clients.Http;

/// <summary>
/// "Connection Retry" feature policy model.
/// </summary>
/// <remarks>
/// Feature should be used together with another configuration model, you should implement it by yourself when used in new model.
/// </remarks>
/// <example>
/// Usage in <c>appsettings.json</c>
/// <code>
/// {
///   "Integration": {
///     "DistributedLock": {
///       "Resilience": {
///         "Enabled": true,
///         "RetryTimeout": "00:00:00.100",
///         "RetryCount": 2
///       }
///     }
///   }
/// }
/// </code>
/// </example>
[ExcludeFromCodeCoverage]
public sealed class ConnectionRetryOptions : PolicyOptions
{
	/// <summary>
	/// Gets allowed retry connection timeout.
	/// </summary>
	/// <value>Defaults to *300 milliseconds*.</value>
	public TimeSpan Timeout { get; init; } = TimeSpan.FromMilliseconds(300);

	/// <summary>
	/// Gets allowed retry count.
	/// </summary>
	/// <remarks>When all tries failed, error will be thrown.</remarks>
	/// <value>Defaults to *3* retries.</value>
	public int RetryCount { get; init; } = 3;
}
