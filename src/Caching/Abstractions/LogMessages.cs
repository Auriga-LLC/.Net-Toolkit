using SharedMessages = Auriga.Toolkit.Logging.LogMessages;

namespace Auriga.Toolkit.Caching;

internal static class CachingLogMessages
{
	private const string StoragePart = "\"{StorageNumber}\"";
	private const string KeyPart = "\"{Key}\"";
	private const string KeysPart = "\"{Keys}\"";
	private const string ValuePart = "\"{Value}\"";
	private const string ValuesPart = "\"{Values}\"";
	private const string KeyValuePart = $"{SharedMessages.Key}:{KeyPart}, {SharedMessages.Value}: {ValuePart}";
	private const string KeyValuesPart = $"{SharedMessages.Key}:{KeyPart}, {SharedMessages.Values}:{ValuesPart}";

	internal const string CalledKey =
		$"{SharedMessages.MethodCalled} ({SharedMessages.Storage}:{StoragePart}, {SharedMessages.Key}:{KeyPart})";

	internal const string CalledKeys =
		$"{SharedMessages.MethodCalled} ({SharedMessages.Storage}:{StoragePart}, {SharedMessages.Keys}:{KeysPart})";

	internal const string CalledKeyValue =
		$"{SharedMessages.MethodCalled} ({SharedMessages.Storage}:{StoragePart}, {KeyValuePart})";

	internal const string CalledKeyValues =
		$"{SharedMessages.MethodCalled} ({SharedMessages.Storage}:{StoragePart}, {KeyValuesPart})";

	internal const string CancelWithKey =
		$"{SharedMessages.MethodCancelled} ({SharedMessages.Storage}:{StoragePart}, {KeyPart})";

	internal const string CancelWithKeys =
		$"{SharedMessages.MethodCancelled} ({SharedMessages.Storage}:{StoragePart}, {KeysPart})";

	internal const string CancelWithKeyValue =
		$"{SharedMessages.MethodCancelled} ({SharedMessages.Storage}:{StoragePart}, {KeyValuePart})";

	internal const string CancelWithKeyValues =
		$"{SharedMessages.MethodCancelled} ({SharedMessages.Storage}:{StoragePart}, {KeyValuesPart})";

	internal const string CacheTypeNotYetSupported = "Not yet supported cache type \"{type}\"";

	internal const string SavingValuesToStorage = "Saving values: {values} to storage: \"{storageNumber}\"";
	internal const string DeletingKeyFromStorage = "Deleting key \"{key}\" from storage \"{storageNumber}\"";
	internal const string DeletingKeysFromStorage = "Deleting keys \"{keys}\" from storage \"{storageNumber}\"";
}
