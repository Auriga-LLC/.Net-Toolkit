using SharedMessages = Auriga.Toolkit.Logging.LogMessages;

namespace Auriga.Toolkit.AspNetCore.Authorization;

/// <summary>
/// Authorization plugin public constants.
/// </summary>
public static class AuthorizationPluginConstants
{
#pragma warning disable CS1591

	public const string FeatureName = "HTTP: Authorization";

	public const string FeatureManual
		= $"{SharedMessages.BaseUrl}/Authorization-QuickStart.md";

#pragma warning restore CS1591
}
