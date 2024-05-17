namespace Auriga.Toolkit.AspNetCore.Authentication;

/// <summary>
/// JWT token helper for parsing.
/// </summary>
/// <param name="Header">Identifies which algorithm is used to generate the signature.</param>
/// <param name="Payload">Set of claims.</param>
/// <param name="Signature">The signature to validate the token.</param>
public sealed record JwtTokenStructureModel(string Header, string Payload, string Signature)
{
	/// <summary>
	/// Checks and splits JWT token into parts.
	/// </summary>
	/// <param name="jwtToken"></param>
	/// <returns>New <see cref="JwtTokenStructureModel"/> from provided token.</returns>
	/// <exception cref="FormatException">If token is not correctly formed.</exception>
	/// <exception cref="ArgumentNullException">If token is empty.</exception>
	public static JwtTokenStructureModel Parse(string jwtToken)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(jwtToken);

		string[] parts = jwtToken.Split('.');
		return parts.Length != 3
			? throw new FormatException("Wrong JWT token model")
			: new JwtTokenStructureModel(parts[0], parts[1], parts[2]);
	}

	/// <inheritdoc />
	public override string ToString()
		=> $"{Header}.{Payload}.{Signature}";
}
