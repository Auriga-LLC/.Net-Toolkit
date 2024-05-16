namespace Auriga.Toolkit.Runtime.Extensions;

/// <summary>
/// Type extension methods.
/// </summary>
public static class TypeExtensions
{
	/// <summary>
	/// Separator string.
	/// </summary>
	internal const string Separator = ", ";

	/// <summary>
	/// Gets type name as "{TypeFullName}, {AssemblyName}".
	/// </summary>
	/// <param name="type">Calling type.</param>
	/// <exception cref="ArgumentNullException">If type is null.</exception>
	/// <returns>String representation of type name.</returns>
	public static string GetFullTypeNameWithAssembly(this Type type)
	{
		ArgumentNullException.ThrowIfNull(type);

		return string.Concat(type.FullName.AsSpan(), Separator.AsSpan(), type.Assembly.GetName().Name.AsSpan());
	}
}
