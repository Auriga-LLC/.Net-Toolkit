using Microsoft.Extensions.DependencyInjection;

namespace Auriga.Toolkit.Clients.Http;

public static class ServiceProviderExtensions
{
	public static T GetNamedServiceOrDefault<T>(this IServiceProvider serviceProvider, string? serviceName)
		where T : notnull
		=> serviceProvider.GetKeyedService<T>(serviceName)
			?? serviceProvider.GetRequiredService<T>();
}
