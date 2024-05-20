using StackExchange.Redis;
using Auriga.Toolkit.Clients.Http;

namespace Auriga.Toolkit.Caching;

public interface ICacheClientProviderFactory : IClientProviderFactory<IConnectionMultiplexer>;
