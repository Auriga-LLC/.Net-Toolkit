using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// Raw JSON payload validator service contract.
/// </summary>
public interface IRawJsonValidator
{
	/// <summary>
	/// Validates JSON string against provided schema.
	/// </summary>
	/// <param name="payload">Json string.</param>
	/// <param name="jsonSchema">Schema, to validate payload against.</param>
	/// <returns><c>True</c> if payload deserialized successfully, otherwise <c>False</c>.</returns>
	OperationContext Validate(string payload, string jsonSchema);

	/// <summary>
	/// Validates JSON string against schema.
	/// </summary>
	/// <typeparam name="T">Requested type.</typeparam>
	/// <param name="payload">Json string.</param>
	/// <param name="strict">Strict mode on? <c>True</c> by default.</param>
	/// <param name="jsonSchema">Optional schema, to validate payload against.</param>
	/// <returns><c>True</c> if payload deserialized successfully, otherwise <c>False</c>.</returns>
	OperationContext Validate<T>(string payload, bool strict = true, string? jsonSchema = null);
}
