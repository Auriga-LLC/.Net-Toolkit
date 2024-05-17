using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Auriga.Toolkit.Runtime;

/// <summary>
/// Enuns helper methods.
/// </summary>
public static class EnumExtensions
{
	private static readonly ConcurrentDictionary<string, string> s_descriptionCache = new();

	/// <summary>
	/// Gets <see cref="DescriptionAttribute"/> value.
	/// </summary>
	/// <param name="value">Current enum value.</param>
	/// <returns>Associated description with enum value.</returns>
	public static string GetDescription(this Enum value)
	{
		ArgumentNullException.ThrowIfNull(value);

		return s_descriptionCache.GetOrAdd(
			$"{value.GetType().FullName}.{value}",
			_ =>
			{
				DescriptionAttribute? name = value.GetType().GetTypeInfo().GetField(value.ToString())
					?.GetCustomAttributes<DescriptionAttribute>(false)
					.FirstOrDefault();

				return name != null ? name.Description : value.ToString();
			});
	}
}
