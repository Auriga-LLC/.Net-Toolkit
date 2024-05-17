using System.Security.Claims;

namespace Auriga.Toolkit.Security;

/// <summary>
/// Claims principal extensions.
/// </summary>
public static class ClaimsPrincipalExtensions
{
	/// <summary>
	/// Gets claim by name.
	/// </summary>
	/// <param name="claimsPrincipal">Current user identity.</param>
	/// <param name="claim">Claim name.</param>
	/// <returns>Claim value.</returns>
	public static string FindFirstValue(this ClaimsPrincipal claimsPrincipal, string claim)
	{
		ArgumentNullException.ThrowIfNull(claimsPrincipal);

		try
		{
			return claimsPrincipal.Claims.First(x => x.Type == claim).Value;
		}
		catch (Exception ex) when (ex is InvalidOperationException)
		{
			return string.Empty;
		}
	}

	/// <summary>
	/// Gets current user id.
	/// </summary>
	/// <param name="claimsPrincipal">Current user identity.</param>
	/// <returns>*Guid* as 'String' from claim.</returns>
	public static string GetCurrentUserIdAsString(this ClaimsPrincipal claimsPrincipal)
	{
		Guid userId = claimsPrincipal.GetCurrentUserId();
		return userId == Guid.Empty ? string.Empty : userId.ToString();
	}

	/// <summary>
	/// Gets current user id.
	/// </summary>
	/// <param name="claimsPrincipal">Current user identity.</param>
	/// <returns>Parsed Guid from claim.</returns>
	public static Guid GetCurrentUserId(this ClaimsPrincipal claimsPrincipal)
	{
		string claimValue = claimsPrincipal.SafeGetIdentityClaim(ClaimTypes.NameIdentifier);
		return Guid.TryParse(claimValue, out Guid result) ? result : Guid.Empty;
	}

	/// <summary>
	/// Gets current username.
	/// </summary>
	/// <param name="claimsPrincipal">Current user identity.</param>
	/// <returns>Parsed username from claim.</returns>
	public static string GetCurrentUserName(this ClaimsPrincipal claimsPrincipal)
		=> claimsPrincipal.SafeGetIdentityClaim(ClaimTypes.Upn);

	/// <summary>
	/// Gets current user full name.
	/// </summary>
	/// <param name="claimsPrincipal">Current user identity.</param>
	/// <returns>Parsed username from claim.</returns>
	public static string GetCurrentUserFullName(this ClaimsPrincipal claimsPrincipal)
		=> claimsPrincipal.SafeGetIdentityClaim(ClaimTypes.Name);

	private static string SafeGetIdentityClaim(this ClaimsPrincipal claimsPrincipal, string claim)
	{
		string claimValue = claimsPrincipal.FindFirstValue(claim);
		return string.IsNullOrWhiteSpace(claimValue) ? string.Empty : claimValue;
	}

	/// <summary>
	/// Gets all involved entity Ids for current HTTP request.
	/// </summary>
	/// <param name="claimsPrincipal">Current user identity.</param>
	/// <returns>List of entity Ids.</returns>
	public static IReadOnlyCollection<Guid> GetAllEntityIds(this ClaimsPrincipal claimsPrincipal)
	{
		ArgumentNullException.ThrowIfNull(claimsPrincipal);

		Claim[] roleClaims = claimsPrincipal.FindAll(ClaimTypes.Role).ToArray();
		var requestedEntities = new List<Guid>(roleClaims.Length)
		{
			// Current user id
			claimsPrincipal.GetCurrentUserId()
		};
		// Current user role ids
		Array.ForEach(
			roleClaims,
			x =>
			{
				if (!Guid.TryParse(x.Value, out Guid roleGuid))
				{
					return;
				}
				requestedEntities.Add(roleGuid);
			});

		return requestedEntities;
	}
}
