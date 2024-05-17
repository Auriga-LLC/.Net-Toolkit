using SharedMessages = Auriga.Toolkit.Logging.LogMessages;

namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// Cache plugins public constants.
/// </summary>
public static class AuthenticationPluginConstants
{
#pragma warning disable CS1591
	public const string FeatureName = "HTTP: Authentication (JWT)";
	public const string FeatureManual = $"{SharedMessages.BaseUrl}/Authentication-QuickStart.md";
#pragma warning restore CS1591
}
