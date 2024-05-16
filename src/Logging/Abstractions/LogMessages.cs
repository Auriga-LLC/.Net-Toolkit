using System.Diagnostics.CodeAnalysis;

namespace Auriga.Toolkit.Logging.Abstractions;

/// <summary>
/// Log messages.
/// </summary>
[ExcludeFromCodeCoverage]
public static class LogMessages
{
#pragma warning disable CS1591

	#region Words

	public const string Called = "Called";
	public const string Cancelled = "Cancelled";
	public const string Completed = "Completed";
	public const string Failed = "Failed";
	public const string Storage = "Storage";
	public const string Key = "Key";
	public const string Keys = "Keys";
	public const string Value = "Value";
	public const string Values = "Values";

	#endregion Words

	#region Generic

	public const string Method = "\"{MethodName}\"";
	public const string MethodCalled = $"{Called} {Method}";
	public const string MethodCancelled = $"{Cancelled} {Method}";
	public const string MethodCompleted = $"{Completed} {Method}";
	public const string MethodFailed = $"{Failed} {Method}";
	public const string MethodFailedWithErrors = $"{MethodFailed} Errors:" + "{errors}";

	#endregion Generic

	#region Exceptions

	public const string DuplicateEntryException = "Cannot insert duplicate key row with unique index.";
	public const string NotExistsEntryException = "An attempt was made to removed a non-existent entity.";
	public const string MalformedRequestException = "The request was built incorrectly.";

	#endregion Exceptions

	#region ManualUrls

	public const string BaseUrl = "https://github.com/Auriga-LLC/.Net-Toolkit/wiki";

	#endregion ManualUrls

	public const string AttributeApplied = "Attribute [{attributeName}] applied to \"{memberName}\"";

#pragma warning restore CS1591
}
