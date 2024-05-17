using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Response parser contract.
/// </summary>
public interface IHttpResponseParser
{
	/// <summary>
	/// Handles response and deserializes into requested type.
	/// </summary>
	/// <typeparam name="TResult">Awaited result type.</typeparam>
	/// <param name="responseParseResult"></param>
	/// <param name="httpResponseMessage">Incoming response message.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous request operation.</returns>
	ValueTask<OperationContext<TResult?>> ParseAsync<TResult>(
		OperationContext<TResult?> responseParseResult,
		HttpResponseMessage httpResponseMessage,
		CancellationToken cancellationToken = default)
		where TResult : class;
}
