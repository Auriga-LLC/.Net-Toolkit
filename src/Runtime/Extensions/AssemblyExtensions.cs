using System.Reflection;

namespace Auriga.Toolkit.Runtime.Extensions;

/// <summary>
/// Assembly helper extension methods.
/// </summary>
public static class AssemblyExtensions
{
	/// <summary>
	/// Gets implementation types of requested contract type.
	/// </summary>
	/// <param name="pluginAssembly">Source assembly where</param>
	/// <typeparam name="T">Type of contract to be resolved.</typeparam>
	/// <returns>An enumerable that contains all implementation types of requested contract type.</returns>
	public static IEnumerable<Type> GetImplementationTypes<T>(this Assembly pluginAssembly)
	{
		ArgumentNullException.ThrowIfNull(pluginAssembly);

		return pluginAssembly.GetTypes().Where(t => t is { IsAbstract: false, IsClass: true } && t.IsAssignableTo(typeof(T)));
	}
}
