using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;
using CoreToolset.Helpers;

namespace CoreToolset.Extensions
{
    public static class StringExtensions
    {
        #region Is, IsNot Extensions

        /// <summary>
        /// Determines whether the specified string is a valid Base64 encoded string.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is a valid Base64 encoded string; otherwise, false.</returns>
        public static bool IsBase64String(this string @this)
        {
            if (string.IsNullOrWhiteSpace(@this))
                return false;

            return @this.Trim().Length % 4 == 0 &&
                   RegexHelper.Base64StringRegex().IsMatch(@this);
        }

        /// <summary>
        /// Determines whether the specified string is a valid GUID.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid GUID; otherwise, false.</returns>
        public static bool IsGuid(this string @this)
        {
            if (string.IsNullOrWhiteSpace(@this))
                return false;

            @this = @this.Trim();

            return Guid.TryParse(@this, out _);
        }

        /// <summary>
        /// Determines whether the specified string is equal to another string using the specified string comparison type.
        /// </summary>
        /// <param name="this">The input string.</param>
        /// <param name="comparingStr">The string to compare with.</param>
        /// <param name="stringComparison">The type of string comparison to use (default: StringComparison.Ordinal).</param>
        /// <returns>true if the specified strings are equal; otherwise, false.</returns>
        public static bool IsEquals(this string @this,
            string comparingStr,
            StringComparison stringComparison = StringComparison.Ordinal)
        {
            if (string.IsNullOrEmpty(@this))
                return false;

            return @this.Trim().Equals(comparingStr, stringComparison);
        }

        /// <summary>
        /// Determines whether the specified string is not null or empty.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is not null or empty; otherwise, false.</returns>
        public static bool IsNotNullOrEmpty(this string @this) =>
            !string.IsNullOrEmpty(@this);


        /// <summary>
        /// Determines whether the specified string is null or empty.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is null or empty; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this string @this)
        {
            return !@this.IsNotNullOrEmpty();
        }

        /// <summary>
        /// Determines whether the specified string is a valid email address.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is a valid email address; otherwise, false.</returns>
        public static bool IsEmailAddress(this string @this)
        {
            if (@this.IsNullOrEmpty())
                return false;

            return RegexHelper.EmailRegex().IsMatch(@this);
        }

        /// <summary>
        /// Determines whether the specified string is a valid phone or fax number.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is a valid phone or fax number; otherwise, false.</returns>
        public static bool IsPhoneOrFaxNumber(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
                return false;

            return RegexHelper.FaxOrPhoneNumberRegex().IsMatch(@this);
        }

        /// <summary>
        /// Determines whether the specified string contains only alphabetic characters and spaces.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string contains only alphabetic characters and spaces; otherwise, false.</returns>
        public static bool IsAlphaAndSpace(this string @this) =>
            RegexHelper.AlphaAndSpaceRegex().IsMatch(@this);


        /// <summary>
        /// Determines whether the specified string contains only alphanumeric characters.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string contains only alphanumeric characters; otherwise, false.</returns>
        public static bool IsAlphaNumeric(this string @this) =>
            !RegexHelper.AlphaNumericRegex().IsMatch(@this);

        /// <summary>
        /// Determines whether the specified string contains only alphanumeric characters.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>false if the specified string contains only alphanumeric characters; otherwise, true.</returns>
        public static bool IsNotAlphaNumeric(this string @this) =>
            !@this.IsAlphaNumeric();

        /// <summary>
        /// Determines whether the specified string is empty.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string is empty; otherwise, false.</returns>
        public static bool IsEmpty(this string @this) =>
            @this is "";

        /// <summary>
        /// Determines whether the specified string contains only numeric characters.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string contains only numeric characters; otherwise, false.</returns>
        public static bool IsNumeric(this string @this) =>
            !RegexHelper.NumericRegex().IsMatch(@this);

        /// <summary>
        /// Determines whether the specified string does not contain any numeric characters.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>true if the specified string does not contain any numeric characters; otherwise, false.</returns>
        public static bool IsNotNumeric(this string @this) =>
            !@this.IsNumeric();

        /// <summary>
        /// Checks whether the string is a valid URL.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid URL, otherwise false.</returns>
        public static bool IsUrl(this string @this) =>
            RegexHelper.UrlRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid URL.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid URL, otherwise false.</returns>
        public static bool IsNotUrl(this string @this) =>
            !@this.IsUrl();

        /// <summary>
        /// Checks whether the string is a valid date in the format "YYYY-MM-DD".
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid date, otherwise false.</returns>
        public static bool IsDate(this string @this) =>
            RegexHelper.DateRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid date in the format "YYYY-MM-DD".
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid date, otherwise false.</returns>
        public static bool IsNotDate(this string @this) =>
            !@this.IsDate();

        /// <summary>
        /// Checks whether the string is a valid IP address.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid IP address, otherwise false.</returns>
        public static bool IsIpAddress(this string @this) =>
            RegexHelper.IpAddressRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid IP address.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid IP address, otherwise false.</returns>
        public static bool IsNotIpAddress(this string @this) =>
            !@this.IsIpAddress();

        /// <summary>
        /// Checks whether the string is a valid postal code.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid postal code, otherwise false.</returns>
        public static bool IsPostalCode(this string @this) =>
            RegexHelper.PostalCodeRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid postal code.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid postal code, otherwise false.</returns>
        public static bool IsNotPostalCode(this string @this) =>
            !@this.IsPostalCode();

        /// <summary>
        /// Checks whether the string is a valid credit card number.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid credit card number, otherwise false.</returns>
        public static bool IsCreditCardNumber(this string @this) =>
            @this.IsNumeric() && RegexHelper.CreditCardNumberRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string is not a valid credit card number.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is not a valid credit card number, otherwise false.</returns>
        public static bool IsNotCreditCardNumber(this string @this) =>
            !@this.IsCreditCardNumber();

        /// <summary>
        /// Checks whether the string matches the MAC address pattern.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid MAC address, otherwise false.</returns>
        public static bool IsMacAddress(this string @this) =>
            RegexHelper.MacAddressRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string matches the MAC address pattern.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a not valid MAC address, otherwise false.</returns>
        public static bool IsNotMacAddress(this string @this) =>
            !@this.IsMacAddress();

        /// <summary>
        /// Checks whether the string matches the IMEI pattern.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a valid IMEI, otherwise false.</returns>
        public static bool IsImei(this string @this) =>
            RegexHelper.ImeiRegex().IsMatch(@this);

        /// <summary>
        /// Checks whether the string matches the IMEI pattern.
        /// </summary>
        /// <param name="this">The string to be checked.</param>
        /// <returns>True if the string is a not valid IMEI, otherwise false.</returns>
        public static bool IsNotImei(this string @this) =>
            !@this.IsImei();
        #endregion

        #region To Extensions

        /// <summary>
        /// Converts the specified string to a decimal number.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>A decimal number representation of the specified string, or 0 if the conversion fails.</returns>
        public static decimal ToDecimal(this string @this)
        {
            if (!@this.IsNullOrEmpty())
            {
                if (decimal.TryParse(@this, out decimal converted))
                    return converted;
            }

            return 0;
        }

        /// <summary>
        /// Converts a string representation to an enumeration value of the specified type.
        /// </summary>
        /// <typeparam name="T">The enumeration type.</typeparam>
        /// <param name="this">The string value to convert.</param>
        /// <param name="ignoreCase">Optional. Determines whether the parsing should be case-insensitive. Default is true.</param>
        /// <returns>The enumeration value of type T.</returns>
        public static T ToEnum<T>(this string @this, bool ignoreCase = true) =>
            (T)Enum.Parse(typeof(T), @this, ignoreCase);

        /// <summary>
        /// Converts the specified string to a FileInfo object.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>A FileInfo object representing the specified string.</returns>
        public static FileInfo ToFileInfo(this string @this) =>
            new(@this);

        /// <summary>
        /// Converts the specified string to a Guid.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>The Guid representation of the string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the specified string is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the specified string cannot be parsed as a Guid.</exception>
        public static Guid ToGuid(this string @this)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(@this);

            if (Guid.TryParse(@this, out var guidResult))
                return guidResult;

            throw new InvalidOperationException(nameof(@this));
        }

        /// <summary>
        /// Converts the specified string to a valid DateTime object or returns null if the conversion fails.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>A DateTime object representing the specified string, or null if the conversion fails.</returns>
        public static DateTime? ToValidDateTimeOrNull(this string @this) =>
            DateTime.TryParse(@this, out DateTime date) ? date : null;

        /// <summary>
        /// Converts the specified string to an XDocument object.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>An XDocument object representing the specified string.</returns>
        public static XDocument ToXDocument(this string @this)
        {
            Encoding encoding = Activator.CreateInstance<ASCIIEncoding>();
            using var ms = new MemoryStream(encoding.GetBytes(@this));
            return XDocument.Load(ms);
        }

        /// <summary>
        /// Converts the specified string to an XmlDocument object.
        /// </summary>
        /// <param name="this">The string to be converted.</param>
        /// <returns>An XmlDocument object representing the specified string.</returns>
        public static XmlDocument ToXmlDocument(this string @this)
        {
            var doc = new XmlDocument();
            doc.LoadXml(@this);
            return doc;
        }

        #endregion

        #region Manipulating Extensions
        public static string CapitalizeFirstLetter(this string @this)
        {
            ArgumentException.ThrowIfNullOrEmpty(@this);

            return char.ToUpper(@this[0]) + @this[1..].ToLower();
        }
        /// <summary>
        /// Removes specified characters from the beginning of a string.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="chars">The characters to be removed.</param>
        /// <returns>A new string with the specified characters removed from the beginning.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the source string is null or empty.</exception>
        public static string RemoveFirstChars(this string @this, params string[] chars)
        {
            ArgumentException.ThrowIfNullOrEmpty(@this);

            var result = @this;

            chars.ToList().ForEach(str =>
            {
                if (str == result.Substring(0, 1))
                    result = result.Substring(1, result.Length - 1);
            });

            return result;
        }

        /// <summary>
        /// Replaces multiple substrings in a string based on specified patterns and replacements.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="sourceDatas">A list of patterns and replacements.</param>
        /// <returns>A new string with the specified substrings replaced.</returns>
        public static string Manipulate(this string @this, List<(string, string)> sourceDatas) =>
            @this.Manipulate(sourceDatas.ToArray());

        /// <summary>
        /// Replaces multiple substrings in a string based on specified patterns and replacements.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="sourceDatas">An array of patterns and replacements.</param>
        /// <returns>A new string with the specified substrings replaced.</returns>
        public static string Manipulate(this string @this, params (string, string)[] sourceDatas)
        {
            var result = @this ?? throw new ArgumentNullException(nameof(@this));

            for (int i = 0; i < sourceDatas.Length; i++)
            {
                if (sourceDatas[i].IsNull()
                    || sourceDatas[i].Item1.IsNull()
                    || sourceDatas[i].Item2.IsNull())
                    break;

                if (Regex.IsMatch(result, sourceDatas[i].Item1))
                    result = Regex.Replace(result, sourceDatas[i].Item1, sourceDatas[i].Item2);
            }

            return result;
        }

        /// <summary>
        /// Splits the specified string into an array of substrings based on a specified separator.
        /// </summary>
        /// <param name="this">The string to be split.</param>
        /// <param name="separator">The separator string.</param>
        /// <param name="option">Options for removing empty entries from the result (default: StringSplitOptions.None).</param>
        /// <returns>An array of substrings.</returns>
        public static string[] Split(this string @this, string separator, StringSplitOptions option = StringSplitOptions.None)
        {
            return @this.Split(new[] { separator }, option);
        }
        #endregion

        /// <summary>
        /// Extracts the domain from an email address.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>The domain portion of the email address.</returns>
        /// <exception cref="Exception">Thrown if the email is null or invalid.</exception>
        public static string ExtractDomainFromEmail(this string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));

            if (!email.IsEmailAddress())
                throw new Exception($"{email} > Email is invalid");

            int indexOfAt = email.IndexOf('@');
            return email[(indexOfAt + 1)..];
        }
    }
}
