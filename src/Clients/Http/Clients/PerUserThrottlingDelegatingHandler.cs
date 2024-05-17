using Microsoft.Extensions.Logging;
using Toolkit.Extensions.DistributedLock.Abstractions;
using Toolkit.Extensions.DistributedLock.Abstractions.Extensions;
using Toolkit.Extensions.Http;

namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Request throttler based on user id.
/// </summary>
/// <remarks>Should be used only with <see cref="UserIdHeaderDelegatingHandler"/>.</remarks>
/// <param name="logger">Logger for current instance.</param>
/// <param name="locker">Shared locker.</param>
public sealed class PerUserThrottlingDelegatingHandler(
	ILogger<PerUserThrottlingDelegatingHandler> logger,
	IDistributedLockProvider locker)
	: DelegatingHandler
{
	/// <inheritdoc/>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		string? userId = request.Headers.GetValues(HeaderName.UserId).FirstOrDefault();
		if (string.IsNullOrWhiteSpace(userId))
		{
			throw new InvalidOperationException("UserId is not provided in headers");
		}

		await using IDistributedLock acquiredLock = await locker.SetLockAsync(userId, cancellationToken)
			.ConfigureAwait(false);

		if (!acquiredLock.IsAcquired)
		{
			logger.LogLockNotAcquired(userId);
			throw new InvalidOperationException("Failed to lock on UserTd");
		}

		return await base.SendAsync(request, cancellationToken)
			.ConfigureAwait(false);
	}
}
