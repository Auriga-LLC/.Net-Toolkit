
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// "JSON String to object" serialization service contract.
/// </summary>
public interface IJsonSerializer
{
	/// <summary>
	/// Try to deserialize asynchronously to json string to desired object type, if possible.
	/// </summary>
	/// <typeparam name="T">Requested type.</typeparam>
	/// <param name="payloadStream">Json stream.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
	/// <returns>A <see cref="ValueTask"/> that represents the asynchronous request operation.</returns>
	ValueTask<OperationContext<T?>> TryDeserializeAsync<T>(Stream payloadStream, CancellationToken cancellationToken = default);

	/// <summary>
	/// Try to deserialize to json string to desired object type, if possible.
	/// </summary>
	/// <typeparam name="T">Requested type.</typeparam>
	/// <param name="payload">Json string.</param>
	/// <param name="strict">Strict mode on? <c>True</c> by default.</param>
	/// <param name="jsonSchema">Optional schema, to validate payload against.</param>
	/// <returns><c>True</c> if payload deserialized successfully, otherwise <c>False</c>.</returns>
	OperationContext<T?> TryDeserialize<T>(string payload, bool strict = true, string? jsonSchema = null);

	/// <summary>
	/// Serializes desired object to json string.
	/// </summary>
	/// <typeparam name="T">Serialized type.</typeparam>
	/// <param name="sourceObject">Source object to be serialized.</param>
	/// <param name="strict">Strict mode on? <c>True</c> by default.</param>
	/// <returns>Json string representation.</returns>
	string Serialize<T>(T sourceObject, bool strict = true);
}
