using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auriga.Toolkit.AspNetCore.Authorization;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection ConfigureAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
		=> services.AddAuthorization();
}
