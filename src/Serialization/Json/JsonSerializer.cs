using System.Text.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Auriga.Toolkit.Logging;
using Auriga.Toolkit.Runtime;
using MSJsonSerializer = System.Text.Json.JsonSerializer;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// "JSON String to object" serialization service.
/// </summary>
/// <param name="logger">Logger service.</param>
/// <param name="rawJsonValidator">Raw JSON validator service.</param>
/// <param name="settingsProvider">JSON Serializer settings provider.</param>
internal sealed class JsonSerializer(
	ILogger<JsonSerializer> logger,
	IRawJsonValidator rawJsonValidator,
	IJsonSerializationSettingsProvider settingsProvider) : IJsonSerializer
{
	/// <inheritdoc/>
	public async ValueTask<OperationContext<T?>> TryDeserializeAsync<T>(Stream payloadStream, CancellationToken cancellationToken = default)
	{
		var deserializationResult = new OperationContext<T?>();
		var payload = await MSJsonSerializer.DeserializeAsync<T>(payloadStream, JsonSerializerOptions.Default, cancellationToken: cancellationToken)
				.ConfigureAwait(false);
		return deserializationResult.SetResult(payload);
	}

	/// <inheritdoc/>
	public OperationContext<T?> TryDeserialize<T>(string payload, bool strict = true, string? jsonSchema = null)
	{
		var operationResult = new OperationContext<T?>();

		try
		{
			var schemaValidation = rawJsonValidator.Validate<T>(payload, strict, jsonSchema);
			if (strict && !schemaValidation.IsSucceed)
			{
				logger.LogMethodFailedWithErrors(nameof(TryDeserialize), schemaValidation.Errors);
				return operationResult.SetErrors(schemaValidation.Errors);
			}

			return operationResult
				.SetResult(
					JsonConvert.DeserializeObject<T>(
						payload,
						settingsProvider.GetSerializerSettings(strict)));
		}
		catch (ArgumentNullException ex)
		{
			logger.LogMethodFailed(nameof(TryDeserialize), ex);
			return operationResult.SetError(ex);
		}
		catch (JsonReaderException ex)
		{
			logger.LogMethodFailed(nameof(TryDeserialize), ex);
			return operationResult.SetError(ex);
		}
		catch (JsonSerializationException ex)
		{
			logger.LogMethodFailed(nameof(TryDeserialize), ex);
			return operationResult.SetError(ex);
		}
	}

	/// <inheritdoc/>
	public string Serialize<T>(T sourceObject, bool strict = true)
		=> JsonConvert.SerializeObject(sourceObject, Formatting.None, settingsProvider.GetSerializerSettings(strict));
}
