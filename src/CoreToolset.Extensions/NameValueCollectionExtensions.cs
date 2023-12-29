using System.Collections.Specialized;

namespace CoreToolset.Extensions
{
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Converts a NameValueCollection to an IEnumerable of KeyValuePair&lt;string, string&gt;.
        /// </summary>
        /// <param name="nvc">The NameValueCollection to convert.</param>
        /// <returns>An IEnumerable of KeyValuePair&lt;string, string&gt; representing the NameValueCollection.</returns>
        public static IEnumerable<KeyValuePair<string, string>> ToIEnumerable(this NameValueCollection @this)
        {
            ArgumentNullException.ThrowIfNull(@this);

            return @this.AllKeys.Select(key => new KeyValuePair<string, string>(key, @this[key]));

        }

        /// <summary>
        /// Determines whether the NameValueCollection contains a specific key, ignoring case sensitivity.
        /// </summary>
        /// <param name="this">The NameValueCollection to search.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>true if the NameValueCollection contains an element with the specified key; otherwise, false.</returns>
        public static bool ContainsKeyIgnoreCase(this NameValueCollection @this, string key)
        {
            ArgumentNullException.ThrowIfNull(@this);

            return @this.AllKeys.Any(k => string.Equals(k, key, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determines whether the NameValueCollection contains a specific key, ignoring case sensitivity.
        /// </summary>
        /// <param name="this">The NameValueCollection to search.</param>
        /// <param name="value">The value to locate.</param>
        /// <returns>true if the NameValueCollection contains an element with the specified key; otherwise, false.</returns>
        public static bool ContainsValueIgnoreKeys(this NameValueCollection @this, string value)
        {
            ArgumentNullException.ThrowIfNull(@this);

            return @this.AllKeys.Any(key => string.Equals(@this[key], value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
