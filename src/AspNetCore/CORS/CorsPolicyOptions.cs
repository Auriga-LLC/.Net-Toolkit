namespace Auriga.Toolkit.AspNetCore.CORS;

/// <summary>
/// CORS policy configuration options.
/// </summary>
public sealed class CorsPolicyOptions
{
	/// <summary>
	/// Gets is CORS policy default.
	/// </summary>
	public bool IsDefault { get; init; }
	
	/// <summary>
	/// Gets is allowed for credentials to be sent over CORS requests.
	/// </summary>
	public bool AllowCredentials { get; init; }
	
	/// <summary>
	/// Gets list of allowed origins.
	/// </summary>
	/// <value><c>Empty</c> by default.</value>
	public IReadOnlyCollection<string>? AllowedOrigins { get; init; }

	/// <summary>
	/// Gets list of allowed methods.
	/// </summary>
	/// <value><c>Empty</c> by default.</value>
	public IReadOnlyCollection<string>? AllowedMethods { get; init; }

	/// <summary>
	/// Gets list of allowed headers.
	/// </summary>
	/// <value><c>Empty</c> by default.</value>
	public IReadOnlyCollection<string>? AllowedHeaders { get; init; }
}
