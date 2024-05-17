namespace Toolkit.Extensions.Clients.Http;

public interface IResponseCacheLifetimeProvider {
	ValueTask<TimeSpan> GetCacheTimeoutAsync(string content, CancellationToken cancellationToken = default);
}
