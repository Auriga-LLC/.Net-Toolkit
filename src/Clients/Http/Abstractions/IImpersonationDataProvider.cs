namespace Toolkit.Extensions.Clients.Http;

/// <summary>
/// Impersonation authentication data provider contract.
/// </summary>
public interface IImpersonationDataProvider
{
	/// <summary>
	///	Gets impersonation data.
	/// </summary>
	/// <remarks>E.g. it can be jwt token.</remarks>
	/// <returns>String representaion of authentication data.</returns>
	string GetImpersonationData();
}
