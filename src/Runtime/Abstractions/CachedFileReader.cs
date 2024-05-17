using System.Collections.Concurrent;

namespace Auriga.Toolkit.Runtime;

/// <summary>
/// Cached file reader.
/// </summary>
public static class CachedFileReader
{
	private static readonly ConcurrentDictionary<string, string> s_fileCache = new();

	/// <summary>
	///
	/// </summary>
	/// <param name="filePath">Requested file path.</param>
	/// <returns></returns>
	public static string ReadFileContents(string filePath)
		=> s_fileCache.GetOrAdd(filePath, File.ReadAllText(filePath));

	/// <summary>
	///
	/// </summary>
	/// <param name="filePath">Requested file path.</param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public static async ValueTask<string> ReadFileContentsAsync(string filePath, CancellationToken cancellationToken = default)
		=> s_fileCache.GetOrAdd(
			filePath,
			await File.ReadAllTextAsync(filePath, cancellationToken)
				.ConfigureAwait(false));
}
