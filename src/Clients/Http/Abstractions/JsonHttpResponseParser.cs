using System.Net;
using Microsoft.Extensions.Logging;
using Toolkit.Extensions.Http;
using Toolkit.Extensions.Runtime;
using Toolkit.Extensions.Serialization.Json;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Default Http response parser.
/// </summary>
/// <param name="logger">Logger service.</param>
/// <param name="serializer">Json serializer service.</param>
public class JsonHttpResponseParser(
	ILogger<JsonHttpResponseParser> logger,
	IJsonSerializer serializer) : IHttpResponseParser
{
	/// <inheritdoc/>
	public ValueTask<OperationContext<TResult?>> ParseAsync<TResult>(
		OperationContext<TResult?> responseParseResult,
		HttpResponseMessage httpResponseMessage,
		CancellationToken cancellationToken = default)
		where TResult : class
	{
		ArgumentNullException.ThrowIfNull(responseParseResult);
		ArgumentNullException.ThrowIfNull(httpResponseMessage);

		return !httpResponseMessage.IsSuccessStatusCode
			? HandleNotSuccessfulResponse(responseParseResult, httpResponseMessage, cancellationToken)
			: HandleSuccessfulResponse(responseParseResult, httpResponseMessage, cancellationToken);
	}

	private async ValueTask<OperationContext<TResult?>> HandleSuccessfulResponse<TResult>(
		OperationContext<TResult?> operationResult,
		HttpResponseMessage httpResponseMessage,
		CancellationToken cancellationToken = default)
		where TResult : class
	{
		if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
		{
			return operationResult;
		}

		if (typeof(TResult) == typeof(string))
		{
			string body = await httpResponseMessage.GetResponseBodyAsync(cancellationToken)
				.ConfigureAwait(false);
			if (string.IsNullOrWhiteSpace(body))
			{
				logger.LogEmptyResponse();
			}

			var resp = body as TResult;
			return operationResult.SetResult(resp);
		}

		await using Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken)
			.ConfigureAwait(false);
		OperationContext<TResult?> deserialized = await serializer.TryDeserializeAsync<TResult>(contentStream, cancellationToken)
			.ConfigureAwait(false);
		return deserialized.IsSucceed
			? operationResult.SetResult(deserialized.Result)
			: operationResult.SetError(DefaultHttpResponseParserLog.Messages.DeserializationFailed);
	}

	private static async ValueTask<OperationContext<TResult?>> HandleNotSuccessfulResponse<TResult>(
		OperationContext<TResult?> operationResult,
		HttpResponseMessage httpResponseMessage,
		CancellationToken cancellationToken = default)
	{
		string body = await httpResponseMessage.GetResponseBodyAsync(cancellationToken)
			.ConfigureAwait(false);
		return operationResult
			.SetError(httpResponseMessage.ReasonPhrase ?? DefaultHttpResponseParserLog.Messages.ReasonPhraseIsNull)
			.SetError(httpResponseMessage.RequestMessage?.ToString() ?? DefaultHttpResponseParserLog.Messages.InitialRequestMessageIsNull)
			.SetError(string.IsNullOrWhiteSpace(body) ? DefaultHttpResponseParserLog.Messages.EmptyResponse : body);
	}
}

internal static partial class DefaultHttpResponseParserLog
{
	[LoggerMessage(
		EventId = 1,
		Level = LogLevel.Debug,
		Message = Messages.NullResponseMessage)]
	public static partial void LogNullResponseMessage(this ILogger logger);

	[LoggerMessage(
		EventId = 2,
		Level = LogLevel.Warning,
		Message = Messages.EmptyResponse)]
	public static partial void LogEmptyResponse(this ILogger logger);

	internal static class Messages
	{
		internal const string NullResponseMessage = "Got null httpResponseMessage";
		internal const string EmptyResponse = "Empty response body";
		internal const string DeserializationFailed = "Deserialization failed";
		internal const string InitialRequestMessageIsNull = "RequestMessage is null";
		internal const string ReasonPhraseIsNull = "ReasonPhrase is null";
	}
}
