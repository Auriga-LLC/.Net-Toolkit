using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// Cached JSON serializer settings provider.
/// </summary>
internal sealed class JsonSerializationSettingsProvider : IJsonSerializationSettingsProvider
{
	private static readonly ConcurrentDictionary<bool, JsonSerializerSettings> s_settingsCache = new();

	/// <inheritdoc/>
	public JsonSerializerSettings GetSerializerSettings(bool strict)
		=> s_settingsCache.GetOrAdd(strict, ConstructSettings);

	private JsonSerializerSettings ConstructSettings(bool strict)
		=> new()
		{
			CheckAdditionalContent = strict,
			ConstructorHandling = ConstructorHandling.Default,
			DefaultValueHandling = DefaultValueHandling.Ignore,
			NullValueHandling = NullValueHandling.Ignore,
			MissingMemberHandling = strict ? MissingMemberHandling.Error : MissingMemberHandling.Ignore,
			ObjectCreationHandling = ObjectCreationHandling.Auto,
			Error = (_, eventArgs) => eventArgs.ErrorContext.Handled = !strict
		};
}
