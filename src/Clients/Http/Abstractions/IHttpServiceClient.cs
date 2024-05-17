using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Generic contract for remote service HTTP-clients.
/// </summary>
public interface IHttpServiceClient : IDisposable
{
	/// <summary>
	/// Executes HTTP request to remote service.
	/// </summary>
	/// <param name="request">Request body/param.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <exception cref="ArgumentNullException">Throws if there is no responseHandler.</exception>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous request operation.</returns>
	ValueTask<OperationContext> ExecuteHttpRequestAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Executes HTTP request to remote service.
	/// </summary>
	/// <typeparam name="TResult">Awaited response type.</typeparam>
	/// <param name="request">Request body/param.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <exception cref="ArgumentNullException">Throws if there is no responseHandler.</exception>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous request operation.</returns>
	ValueTask<OperationContext<TResult?>> ExecuteHttpRequestAsync<TResult>(
		HttpRequestMessage request,
		CancellationToken cancellationToken = default)
		where TResult : class;
}
