using System.Globalization;
using System.Net.Http.Headers;
using Toolkit.Extensions.Runtime;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Assembly helper methods class.
/// </summary>
public static class TypeExtensions
{
	private const string UserAgentHeaderTemplate = "{0}/{1}";	
	
	/// <summary>
	/// Builds product version in UserAgent manner.
	/// </summary>
	/// <param name="productType">Type of product.</param>
	/// <param name="productName">Optional product name. If not provided will be used short type name.</param>
	/// <returns>Product version like <c>Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0</c>.</returns>
	public static ProductInfoHeaderValue BuildUserAgentHeader(this Type productType, string? productName = null)
	{
		ArgumentNullException.ThrowIfNull(productType);

		string version = productType.Assembly.GetInformationalVersion();
		return ProductInfoHeaderValue.Parse(
			string.Format(CultureInfo.InvariantCulture, UserAgentHeaderTemplate, productName ?? productType.FullName, version));
	}
}
