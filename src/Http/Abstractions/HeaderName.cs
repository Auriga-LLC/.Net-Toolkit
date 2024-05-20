using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Http;

/// <summary>
/// List of well known HTTP-header names.
/// </summary>
[ExcludeFromCodeCoverage]
public static class HeaderName
{
	/// <summary>
	/// Header marks proxy buffering enablement.
	/// </summary>
	public const string ProxyBuffering = "X-Accel-Buffering";

	/// <summary>
	/// Header marks that request is being called from orchestrator.
	/// </summary>
	/// <remarks>Set to true, if its already being handled by CQRS.</remarks>
	public const string HandledByOrchestrator = "X-CQRS-ByPass";

	/// <summary>
	/// Unique request id header name.
	/// </summary>
	/// <remarks>Should be set at gateway.</remarks>
	public const string RequestId = "X-Request-Id";

	/// <summary>
	/// Users Id header name.
	/// </summary>
	public const string UserId = "X-User-Id";

	/// <summary>
	/// Users secret header name.
	/// </summary>
	public const string UserSecret = "X-User-Secret";

	/// <summary>
	/// Request executor header name.
	/// </summary>
	/// <remarks>Should be set only for requests initiated by services.</remarks>
	public const string RequestExecutor = "X-Request-Executor";

	/// <summary>
	/// Request authorization mode header name.
	/// </summary>
	public const string RequestAuthorizationMode = "X-Request-Authorization-Mode";
}
