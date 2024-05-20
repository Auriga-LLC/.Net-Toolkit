using Microsoft.Extensions.Options;

namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Application name provider.
/// </summary>
/// <param name="configuration">Configuration model.</param>
internal sealed class ApplicationNameProvider(IOptions<ApplicationConfiguration> configuration)
	: IApplicationNameProvider
{
	/// <inheritdoc/>
	public string GetApplicationName()
		=> configuration.Value.Name ?? throw new InvalidOperationException("Application name is not set in config");
}
