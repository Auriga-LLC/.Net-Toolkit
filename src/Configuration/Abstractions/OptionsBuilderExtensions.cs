using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Extension methods for setting up configurations in an <see cref="IServiceCollection"/>.
/// </summary>
public static class OptionsBuilderExtensions
{
	/// <summary>
	/// Registers configuration options to the specified <see cref="IServiceCollection"/> and binds it to configuration section.
	/// </summary>
	/// <typeparam name="T">Configuration type.</typeparam>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <param name="sectionName">Configuration section name.</param>
	/// <exception cref="ArgumentNullException">If configuration section name is not provided.</exception>
	/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
	public static IServiceCollection ConfigureOptions<T>(
		this IServiceCollection services,
		IConfiguration configuration,
		string sectionName)
		where T : class
	{
		var configSection = configuration.CheckConfigurationExistanceInternal(sectionName);
		if (configSection == null)
		{
			return services;
		}

		return RegisterConfigurationIfExistsInternal<T>(services, configSection);
	}

	/// <summary>
	/// Gets configuration options from the specified <see cref="IConfiguration"/> and binds it to class.
	/// </summary>
	/// <typeparam name="T">Configuration type.</typeparam>
	/// <param name="configuration">The <see cref="IConfiguration"/> to read configuration from.</param>
	/// <param name="sectionName">Configuration section name.</param>
	/// <exception cref="ArgumentNullException">If configuration section name is not provided.</exception>
	/// <returns>Configuration options mapped to class.</returns>
	public static T? GetConfiguration<T>(this IConfiguration configuration, string sectionName) where T : class
	{
		var configSection = configuration.CheckConfigurationExistanceInternal(sectionName);
		if (configSection == null)
		{
			return null;
		}

		return configSection.Get<T>();
	}

	private static IConfigurationSection? CheckConfigurationExistanceInternal(this IConfiguration configuration, string sectionName)
	{
		ArgumentNullException.ThrowIfNull(configuration);
		ArgumentException.ThrowIfNullOrWhiteSpace(sectionName);

		var configSection = configuration.GetSection(sectionName);
		return !configSection.Exists() ? null : configSection;
	}

	private static IServiceCollection RegisterConfigurationIfExistsInternal<T>(IServiceCollection services, IConfiguration configSection)
		where T : class
	{
		// Prevent duplicated registration in DI
		var alreadyRegistered = services.FirstOrDefault(x => x.ServiceType == typeof(IConfigureOptions<T>));
		if (alreadyRegistered != null)
		{
			return services;
		}

		// Register config from settings in DI
		return services.Configure<T>(configSection);
	}
}
