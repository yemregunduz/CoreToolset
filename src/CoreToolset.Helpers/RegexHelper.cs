using System.Text.RegularExpressions;

namespace CoreToolset.Helpers
{
    public static partial class RegexHelper
    {
        [GeneratedRegex("^[a-zA-Z0-9\\+/]*={0,3}$", RegexOptions.None)]
        public static partial Regex Base64StringRegex();

        [GeneratedRegex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$", RegexOptions.IgnoreCase)]
        public static partial Regex EmailRegex();

        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        public static partial Regex PasswordRegex();

        [GeneratedRegex("^\\s*(?:\\+?(\\d{1,3}))?[-.(]*(\\d{3})[-.)]*(\\d{3})[-.]*(\\d{4})(?: *x(\\d+))?\\s*$", RegexOptions.None)]
        public static partial Regex FaxOrPhoneNumberRegex();

        [GeneratedRegex("^[a-zA-Z ]+$")]
        public static partial Regex AlphaAndSpaceRegex();

        [GeneratedRegex("[^a-zA-Z0-9]")]
        public static partial Regex AlphaNumericRegex();

        [GeneratedRegex("[^0-9]")]
        public static partial Regex NumericRegex();

        [GeneratedRegex(@"^(http|https):\/\/([\w-]+\.)+[\w-]+(\/[\w-./?%&=]*)?$", RegexOptions.IgnoreCase)]
        public static partial Regex UrlRegex();

        [GeneratedRegex(@"^((0?[1-9]|1[0-2])\/(0?[1-9]|1\d|2[0-8])|((0?[13-9]|1[0-2])\/(29|30))|(0?2\/(0?[1-9]|1\d|2[0-8])))\/((20\d{2})|(19\d{2}))$", RegexOptions.IgnoreCase)]
        public static partial Regex DateRegex();

        [GeneratedRegex(@"^\d{5}(?:[-\s]\d{4})?$")]
        public static partial Regex PostalCodeRegex();

        [GeneratedRegex(@"^\b(?:\d{1,3}\.){3}\d{1,3}\b$")]
        public static partial Regex IpAddressRegex();

        [GeneratedRegex(@"^(?:\d{4}-){3}\d{4}$")]
        public static partial Regex CreditCardNumberRegex();

        [GeneratedRegex(@"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$", RegexOptions.IgnoreCase)]
        public static partial Regex MacAddressRegex();

        [GeneratedRegex(@"^([0-9]{15}[\ ]{1})|([0-9]{14}[\/]?[0-9]{2})([\ $]{1})", RegexOptions.IgnoreCase)]
        public static partial Regex ImeiRegex();

    }
}
