using CoreToolset.ExtensionsTests.TestCases;
using System.Text;

namespace CoreToolset.ExtensionsTests
{
    [TestFixture]
    public class StringExtensionsTests
    {

        #region IsBase64String
        [TestCaseSource(typeof(StringExtensionsTestCases), nameof(StringExtensionsTestCases.IsBase64StringTestCases))]
        public bool IsBase64String_TestCases(string inputString)
        {
            bool result = inputString.IsBase64String();
            return result;
        }
        #endregion

        #region IsEmailAddress
        [TestCaseSource(typeof(StringExtensionsTestCases), nameof(StringExtensionsTestCases.IsEmailAddressTestCases))]
        public bool IsEmailAddress_TestCases(string inputString)
        {
            bool result = inputString.IsEmailAddress();
            return result;
        }
        #endregion

        #region IsGuid
        [TestCaseSource(typeof(StringExtensionsTestCases), nameof(StringExtensionsTestCases.IsGuidTestCases))]
        public bool IsGuid_TestCases(string inputString)
        {
            bool result = inputString.IsGuid();
            return result;
        }
        #endregion

        #region IsEquals

        [TestCaseSource(typeof(StringExtensionsTestCases), nameof(StringExtensionsTestCases.IsEqualsTestCases))]
        public bool IsEquals_TestCases(string inputString, string comparingString, StringComparison comparisonType = StringComparison.Ordinal)
        {
            bool result = inputString.IsEquals(comparingString, comparisonType);
            return result;
        }
        #endregion

        #region IsNotNullOrEmpty
        [TestCaseSource(typeof(StringExtensionsTestCases), nameof(StringExtensionsTestCases.IsNotNullOrEmptyTestCases))]
        public bool IsNotNullOrEmpty_TestCases(string inputString)
        {
            bool result = inputString.IsNotNullOrEmpty();
            return result;
        }
        #endregion

        #region IsNullOrEmpty
        [TestCaseSource(typeof(StringExtensionsTestCases), nameof(StringExtensionsTestCases.IsNullOrEmptyTestCases))]
        public bool IsNullOrEmpty_TestCases(string inputString)
        {
            bool result = inputString.IsNullOrEmpty();
            return result;
        }
        #endregion

        #region IsPhoneOrFaxNumber
        [TestCaseSource(typeof(StringExtensionsTestCases), nameof(StringExtensionsTestCases.IsPhoneOrFaxNumberTestCases))]
        public bool IsPhoneOrFaxNumber_TestCases(string inputString)
        {
            bool result = inputString.IsPhoneOrFaxNumber();
            return result;
        }
        #endregion

    }


}
