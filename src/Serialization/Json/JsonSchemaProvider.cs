using System.Collections.Concurrent;
using System.Globalization;
using NJsonSchema;
using NJsonSchema.NewtonsoftJson.Generation;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// JSON schema provider implementation based on NJsonSchema.
/// </summary>
/// <param name="serializationSettingsProvider">Serialization settings provider.</param>
internal sealed class JsonSchemaProvider(IJsonSerializationSettingsProvider serializationSettingsProvider) : IJsonSchemaProvider
{
	private static readonly ConcurrentDictionary<string, JsonSchema> s_typeSchemaCache = new();

	private static readonly ConcurrentDictionary<bool, NewtonsoftJsonSchemaGeneratorSettings>
		s_schemaGeneratorSettingsCache = new();

	/// <inheritdoc/>
	public JsonSchema GetJsonSchema<T>(bool strict) =>
		s_typeSchemaCache.GetOrAdd(
			typeof(T).FullName!,
			_ => NewtonsoftJsonSchemaGenerator.FromType<T>(
				s_schemaGeneratorSettingsCache.GetOrAdd(
					strict,
					_ => new NewtonsoftJsonSchemaGeneratorSettings
					{
						SerializerSettings = serializationSettingsProvider.GetSerializerSettings(strict),
						ResolveExternalXmlDocumentation = false,
						FlattenInheritanceHierarchy = true,
						UseXmlDocumentation = false,
						AlwaysAllowAdditionalObjectProperties = !strict
					})));

	/// <inheritdoc/>
	public JsonSchema GetJsonSchema(string jsonSchema)
	{
		string jsonSchemakey = jsonSchema.GetHashCode(StringComparison.InvariantCultureIgnoreCase)
			.ToString(CultureInfo.InvariantCulture);

		return s_typeSchemaCache.GetOrAdd(
			jsonSchemakey,
			_ => JsonSchema.FromJsonAsync(jsonSchema, CancellationToken.None).Result);
	}
}
