namespace Auriga.Toolkit.Clients.Http;

/// <summary>
///
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IClientProviderFactory<out T>
{
	/// <summary>
	///
	/// </summary>
	/// <returns></returns>
	T CreateClient();
}
