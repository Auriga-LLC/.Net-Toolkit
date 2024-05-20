using NJsonSchema;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// JSON schema provider contract.
/// </summary>
internal interface IJsonSchemaProvider
{
	/// <summary>
	/// Generate Json schema definition based from istance of <typeparamref name="T"/>.
	/// </summary>
	/// <typeparam name="T">Referenced object type.</typeparam>
	/// <param name="strict">Strict mode on? <c>True</c> by default.</param>
	/// <returns><see cref="JsonSchema"/> schema generated from provided class reference.</returns>
	JsonSchema GetJsonSchema<T>(bool strict);

	/// <summary>
	/// Generate Json schema definition based from string representation.
	/// </summary>
	/// <param name="jsonSchema">Serialized schema.</param>
	/// <returns><see cref="JsonSchema"/> schema generated from provided class reference.</returns>
	JsonSchema GetJsonSchema(string jsonSchema);
}
