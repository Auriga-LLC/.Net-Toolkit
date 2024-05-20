using Microsoft.Extensions.Logging;
using NJsonSchema;
using Auriga.Toolkit.Logging;
using Auriga.Toolkit.Runtime;

namespace Auriga.Toolkit.Serialization.Json;

/// <summary>
/// Raw JSON payload validator service.
/// </summary>
/// <param name="logger">Logger service.</param>
/// <param name="schemaProvider">Json schema provider service.</param>
internal sealed class RawJsonValidator(ILogger<RawJsonValidator> logger, IJsonSchemaProvider schemaProvider) : IRawJsonValidator
{
	/// <inheritdoc/>
	public OperationContext Validate(string payload, string jsonSchema)
	{
		var operationResult = new OperationContext();

		if (string.IsNullOrWhiteSpace(payload))
		{
			return operationResult.SetErrors("Empty payload");
		}

		if (string.IsNullOrWhiteSpace(jsonSchema))
		{
			return operationResult.SetErrors("Empty jsonSchema");
		}

		JsonSchema schema = schemaProvider.GetJsonSchema(jsonSchema);
		return ValidateInternal(operationResult, schema, payload);
	}

	/// <inheritdoc/>
	public OperationContext Validate<T>(string payload, bool strict = true, string? jsonSchema = null)
	{
		var operationResult = new OperationContext();

		if (string.IsNullOrWhiteSpace(payload))
		{
			return operationResult.SetErrors("Empty payload");
		}

		JsonSchema schema = !string.IsNullOrWhiteSpace(jsonSchema)
			? schemaProvider.GetJsonSchema(jsonSchema)
			: schemaProvider.GetJsonSchema<T>(strict);
		return ValidateInternal(operationResult, schema, payload);
	}

	private OperationContext ValidateInternal(OperationContext operationResult, JsonSchema schema, string payload)
	{
		ArgumentNullException.ThrowIfNull(schema);

		IList<string> errorMessages = schema.Validate(payload).Select(e => e.ToString()).ToArray();
		if (errorMessages.Any())
		{
			logger.LogMethodFailedWithErrors(nameof(Validate), errorMessages);
			return operationResult.SetErrors(errorMessages);
		}

		return operationResult;
	}
}
