namespace Auriga.Toolkit.Clients.Http;

internal static class LogMessages
{
	internal const string AnonymousRequest = "Executing request anonymously";
	internal const string ExecutingRequest = "Executing request: method=\"{requestMethod}\" url=\"{requestUri}\" headers=\"{requestHeaders}\" body=\"{requestBody}\"";
	internal const string ReceivedResponse = "Received response: statusCode=\"{responseStatusCode}\" headers=\"{responseHeaders}\" body=\"{responseContent}\"";
	internal const string ErrorResponse = "Error reading response: {response}";
	internal const string CorrectResponse = "Got response: {response}";
	internal const string FailedToAddRequestHeader = "Failed to add request header \"{headerName}\":\"{headerValue}\"";
	internal const string HeaderIsAlreadySet = "Failed to set request header \"{headerName}\", because its already set";
}
