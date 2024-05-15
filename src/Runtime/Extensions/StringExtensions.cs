using System.Text;
using InterpolatedStringFormatter;

namespace Auriga.Toolkit.Runtime.Extensions;

/// <summary>
/// String extensions.
/// </summary>
public static class StringExtensions
{
	private static readonly string s_ellipsisChar = ((char)0x2026).ToString();

	/// <summary>
	/// Converts interpolated string to <see cref="string.Format(System.IFormatProvider?,string,object?)"/> and builds result string.
	/// </summary>
	/// <param name="interpolatedString">Interpolated string.</param>
	/// <param name="args">An object array that contains zero or more objects to format.</param>
	/// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
	public static string FormatInterpolatedString(this string interpolatedString, params object[] args)
	{
		ArgumentNullException.ThrowIfNull(args);

		if (args.Length == 0)
		{
			return interpolatedString;
		}

		return interpolatedString.Interpolate(args);
	}

	/// <summary>
	/// Truncates string adding ellipsis char at the end.
	/// </summary>
	/// <param name="logMessage">Source text message/</param>
	/// <param name="ellipsisChar">Ellipsis char.</param>
	/// <param name="maxLength">Maximum allowed length.</param>
	/// <returns>Truncated string with ellipsis char at the end.</returns>
	public static string Truncate(this string? logMessage, string? ellipsisChar, int maxLength = 1000)
		=> logMessage?.Length < maxLength
			? logMessage
			: string.Concat(logMessage.AsSpan()[..maxLength], (ellipsisChar ?? s_ellipsisChar).AsSpan());

	/// <summary>
	/// Encodes string into <c>base64</c>.
	/// </summary>
	/// <param name="toEncode">String to be encoded.</param>
	/// <returns><c>base64</c> encoded string.</returns>
	public static string EncodeToBase64(this string toEncode)
		=> Convert.ToBase64String(Encoding.UTF8.GetBytes(toEncode));

	/// <summary>
	/// Decodes string from <c>base64</c>.
	/// </summary>
	/// <param name="encodedData">String to be decoded.</param>
	/// <returns>Decoded string.</returns>
	public static string DecodeFrom64(this string encodedData)
		=> Encoding.UTF8.GetString(Convert.FromBase64String(encodedData));
}
