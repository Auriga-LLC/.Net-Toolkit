namespace Auriga.Toolkit.Http;

public static class HttpResponseMessageExtensions
{
	public static async ValueTask<string> GetResponseBodyAsync(
		this HttpResponseMessage httpResponseMessage,
		CancellationToken cancellationToken = default)
	{
		ArgumentNullException.ThrowIfNull(httpResponseMessage);

		using HttpContent content = httpResponseMessage.Content;
		return await content.ReadAsStringAsync(cancellationToken)
			.ConfigureAwait(false);
	}
}
