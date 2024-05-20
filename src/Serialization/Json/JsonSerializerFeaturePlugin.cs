using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auriga.Toolkit.Plugins;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// JSON (De)Serialization feature plugin.
/// </summary>
internal sealed class JsonSerializerFeaturePlugin : FeaturePlugin, IServiceConfiguratorPlugin
{
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override string Name => "(De)Serialization: JSON";

	/// <inheritdoc/>
	public override int LoadOrder => (int)PluginLoadOrder.Independent;

	/// <inheritdoc/>
	public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		return services
			.AddSingleton<IJsonSerializationSettingsProvider, JsonSerializationSettingsProvider>()
			.AddSingleton<IJsonSchemaProvider, JsonSchemaProvider>()
			.AddTransient<IJsonSerializer, JsonSerializer>()
			.AddTransient<IRawJsonValidator, RawJsonValidator>();
	}
}
