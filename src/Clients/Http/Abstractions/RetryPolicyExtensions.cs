using Polly;
using Polly.Extensions.Http;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Retry policy extensions.
/// </summary>
public static class RetryPolicyExtensions
{
	/// <summary>
	/// Builds "retry requests" policy for HTTP requests.
	/// </summary>
	/// <param name="config">Remote service connection configuration.</param>
	/// <param name="applicationName">Application name.</param>
	/// <returns>Built policy or 1-time fallback.</returns>
	public static IAsyncPolicy<HttpResponseMessage> BuildRetryPolicy(this ClientConnectionOptions config, string applicationName)
	{
		ArgumentNullException.ThrowIfNull(config);

		if (!config.Resilience.Enabled)
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.RetryAsync()
				.WithPolicyKey(applicationName);
		}

		return HttpPolicyExtensions
			.HandleTransientHttpError()
			.WaitAndRetryAsync(
				config.Resilience.RetryCount,
				retryAttempt => TimeSpan.FromSeconds(Math.Pow(config.Resilience.Timeout.TotalSeconds, retryAttempt)))
			.WithPolicyKey(applicationName);
	}
}
