using Microsoft.Extensions.Logging;
using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Generic remote service HTTP-client.
/// </summary>
/// <param name="logger">Logger for current instance.</param>
/// <param name="httpClient">Http client for requesting data.</param>
/// <param name="httpResponseParser">Response parser service.</param>
public abstract class HttpServiceClient(
	ILogger logger,
	HttpClient httpClient,
	IHttpResponseParser httpResponseParser)
	: IHttpServiceClient
{
	private bool _alreadyDisposed;

	/// <inheritdoc/>
	public async ValueTask<OperationContext> ExecuteHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
	{
		var httpRequestResult = new OperationContext();

		try
		{
			using HttpResponseMessage responseMessage = await httpClient
				.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
				.ConfigureAwait(false);
			if (!responseMessage.IsSuccessStatusCode)
			{
				logger.LogErrorReadingResponse(responseMessage);
				return httpRequestResult.SetError(responseMessage.ReasonPhrase);
			}

			return httpRequestResult;
		}
		catch (TaskCanceledException ex)
		{
			httpClient.CancelPendingRequests();
			return httpRequestResult.SetError(ex);
		}
		catch (HttpRequestException ex)
		{
			return httpRequestResult.SetError(ex);
		}
	}

	/// <inheritdoc/>
	public async ValueTask<OperationContext<TResult?>> ExecuteHttpRequestAsync<TResult>(
		HttpRequestMessage request,
		CancellationToken cancellationToken = default)
		where TResult : class
	{
		var httpRequestResult = new OperationContext<TResult?>();

		try
		{
			using HttpResponseMessage responseMessage = await httpClient
				.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
				.ConfigureAwait(false);

			OperationContext<TResult?> parsedResponse = await httpResponseParser.ParseAsync(httpRequestResult, responseMessage, cancellationToken)
				.ConfigureAwait(false);
			if (!parsedResponse.IsSucceed)
			{
				logger.LogErrorReadingResponse(responseMessage);
			}

			return parsedResponse;
		}
		catch (TaskCanceledException ex)
		{
			httpClient.CancelPendingRequests();
			return httpRequestResult.SetError(ex);
		}
		catch (HttpRequestException ex)
		{
			return httpRequestResult.SetError(ex);
		}
	}

	/// <inheritdoc cref="IDisposable.Dispose"/>
	protected virtual void Dispose(bool disposing)
	{
		if (_alreadyDisposed)
		{
			return;
		}

		if (disposing)
		{
			httpClient.Dispose();
		}

		// TODO: free unmanaged resources (unmanaged objects) and override finalizer
		// TODO: set large fields to null
		_alreadyDisposed = true;
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
