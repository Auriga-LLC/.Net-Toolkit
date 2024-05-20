namespace Auriga.Toolkit.Runtime;

public static class UriExtensions
{
	private const char TrailingSlash = '/';

	public static string GetNormalizedAbsoluteUri(this Uri endpoint, bool useTrailingSlash = true)
	{
		if (useTrailingSlash)
		{
			return endpoint.AbsoluteUri.EndsWith(TrailingSlash)
				? endpoint.AbsoluteUri
				: $"{endpoint.AbsoluteUri}{TrailingSlash}";
		}

		return endpoint.AbsolutePath.EndsWith(TrailingSlash)
			? endpoint.AbsoluteUri.Substring(0, endpoint.AbsoluteUri.Length - 1)
			: endpoint.AbsoluteUri;
	}
}
