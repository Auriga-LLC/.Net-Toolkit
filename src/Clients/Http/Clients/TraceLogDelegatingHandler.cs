using Microsoft.Extensions.Logging;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Requests logging handler.
/// </summary>
/// <remarks>Should be used only with <see cref="PerUserThrottlingDelegatingHandler"/>.</remarks>
/// <param name="logger">Logger service.</param>
/// <param name="serviceConfiguration">Service client configuration.</param>
internal sealed class TraceLogDelegatingHandler(
	ILogger logger,
	ClientConnectionOptions serviceConfiguration)
	: DelegatingHandler
{
	/// <inheritdoc/>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (!serviceConfiguration.RequestTracing.Enabled)
		{
			return await base.SendAsync(request, cancellationToken)
				.ConfigureAwait(false);
		}

		HttpResponseMessage? response = null;
		try
		{
			response = await base.SendAsync(request, cancellationToken)
				.ConfigureAwait(false);
		}
		finally
		{
			// Finally, we check if we decided to log HttpClient request/response or not.
			// only if we want to, we will have some allocations for the logger and try to read headers and contents.
			if (request.Content != null)
			{
				logger.LogExecutingRequest(request.Method, request.RequestUri, request.Headers.ToDictionary(), await request.Content.ReadAsStringAsync(cancellationToken)
					.ConfigureAwait(false));
			}

			if (response?.Content != null)
			{
				logger.LogReceivedResponse(response.StatusCode, response.Headers.ToDictionary(), await response.Content.ReadAsStringAsync(cancellationToken)
					.ConfigureAwait(false));
			}
		}

		return response;
	}
}
