namespace Auriga.Toolkit.Configuration;

/// <summary>
/// Application name provider contract.
/// </summary>
public interface IApplicationNameProvider
{
	/// <summary>
	/// Gets application name.
	/// </summary>
	/// <returns>Application name.</returns>
	string GetApplicationName();
}
