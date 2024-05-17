namespace Auriga.Toolkit.Logging;

/// <summary>
/// Generic ASP.NET related messages.
/// </summary>
public static class AspNetCoreLogMessages
{
#pragma warning disable CS1591

	public const string HttpContextNotFound = "HttpContext is not available";
	public const string HttpContextLost = "Something went wrong and http context is missing";

#pragma warning restore CS1591
}
