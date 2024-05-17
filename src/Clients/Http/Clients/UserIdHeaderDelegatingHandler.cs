///
/*using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Toolkit.Extensions.AspNetCore.Authentication.Extensions;
using Toolkit.Extensions.Http;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Requests userId include handler.
/// </summary>
/// <remarks>Should be used only with <see cref="PerUserThrottlingDelegatingHandler"/>.</remarks>
/// <param name="logger">Logger for current instance.</param>
/// <param name="httpContextAccessor">Http context acessor.</param>
public sealed class UserIdHeaderDelegatingHandler(
	ILogger<UserIdHeaderDelegatingHandler> logger,
	IHttpContextAccessor httpContextAccessor)
	: DelegatingHandler
{
	/// <inheritdoc/>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		string? userId = httpContextAccessor.HttpContext?.User.GetCurrentUserIdAsString();
		if (string.IsNullOrWhiteSpace(userId))
		{
			throw new InvalidOperationException("User is not provided");
		}

		if (!request.Headers.TryAddWithoutValidation(HeaderName.UserId, userId))
		{
			logger.LogFailedToAddRequestHeader(HeaderName.UserId, userId);
		}

		return await base.SendAsync(request, cancellationToken)
			.ConfigureAwait(false);
	}
}
*/
