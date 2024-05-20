using Newtonsoft.Json;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// JSON serializer settings provider contract.
/// </summary>
internal interface IJsonSerializationSettingsProvider
{
	/// <summary>
	/// Gets serializer settings.
	/// </summary>
	/// <param name="strict">Strict mode on? <c>False</c> by default.</param>
	/// <returns><see cref="JsonSerializerSettings"/> instance.</returns>
	JsonSerializerSettings GetSerializerSettings(bool strict);
}
