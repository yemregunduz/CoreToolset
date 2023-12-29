using System.ComponentModel;

namespace CoreToolset.Extensions
{
    public static class EnumExtensions
    {

        /// <summary>
        /// Retrieves all values of the specified enum type.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>An enumerable containing all the enum values.</returns>
        public static IEnumerable<T> GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Retrieves the description of the specified enum value.
        /// If the enum value has a [Description] attribute, the description is taken from that attribute.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="this">The enum value.</param>
        /// <returns>The description of the enum value.</returns>
        public static string GetDescription<T>(this T @this) where T : Enum
        {
            var memberInfo = typeof(T).GetMember(@this.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return ((DescriptionAttribute)attributes[0]).Description;

            return @this.ToString();
        }

        /// <summary>
        /// Retrieves a specific attribute for the specified enum value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute to retrieve.</typeparam>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>The attribute attached to the enum value, or <c>null</c> if the attribute is not found.</returns>
        public static TAttribute GetAttribute<TAttribute, TEnum>(this TEnum enumValue)
            where TAttribute : Attribute
            where TEnum : Enum
        {
            var memberInfo = typeof(TEnum).GetMember(enumValue.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(TAttribute), false);

            if (attributes.Length > 0)
                return (TAttribute)attributes[0];

            return null;
        }

        /// <summary>
        /// Determines whether the specified enum value contains a specific flag.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <param name="flag">The flag value to check for.</param>
        /// <returns><c>true</c> if the enum value contains the specified flag; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException">Thrown when the type parameter 'T' is not an enum.</exception>
        public static bool HasFlag<T>(this T value, T flag) where T : Enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("The 'T' type must be an enum.");

            dynamic dynamicValue = value;
            dynamic dynamicFlag = flag;

            return (dynamicValue & dynamicFlag) == dynamicFlag;
        }

        /// <summary>
        /// Checks whether the specified value is present in the provided array.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The value to check.</param>
        /// <param name="array">The array to search in.</param>
        /// <returns><c>true</c> if the value is present in the array; otherwise, <c>false</c>.</returns>
        public static bool IsIn<T>(this T @this, params T[] array) =>
            array.Contains(@this);

        /// <summary>
        /// Parses an enum value from a string case-insensitively.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The parsed enum value.</returns>
        public static T ParseCaseInsensitive<T>(string value)
            where T : Enum =>
                (T)Enum.Parse(typeof(T), value, true);

    }
}
