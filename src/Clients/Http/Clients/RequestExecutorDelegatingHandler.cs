using Microsoft.Extensions.Logging;
using Toolkit.Extensions.Configuration;
using Toolkit.Extensions.Http;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Requests userExecutor include handler.
/// </summary>
/// <remarks>Should be used only with <see cref="PerUserThrottlingDelegatingHandler"/>.</remarks>
/// <param name="logger">Logger for current instance.</param>
/// <param name="applicationNameProvider">Application name provider.</param>
internal sealed class RequestExecutorDelegatingHandler(
	ILogger<RequestExecutorDelegatingHandler> logger,
	IApplicationNameProvider applicationNameProvider)
	: DelegatingHandler
{
	/// <inheritdoc/>
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (!request.Headers.TryAddWithoutValidation(HeaderName.RequestExecutor, applicationNameProvider.GetApplicationName()))
		{
			logger.LogFailedToAddRequestHeader(HeaderName.RequestExecutor, applicationNameProvider.GetApplicationName());
		}

		return base.SendAsync(request, cancellationToken);
	}
}
