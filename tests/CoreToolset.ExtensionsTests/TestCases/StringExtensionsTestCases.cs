using System.Text;

namespace CoreToolset.ExtensionsTests.TestCases
{
    public class StringExtensionsTestCases
    {
        #region IsBase64StringTestCases
        public static IEnumerable<TestCaseData> IsBase64StringTestCases
        {
            get
            {
                string validBase64String = Convert.ToBase64String(Encoding.UTF8.GetBytes("TestString"));
                yield return new TestCaseData(validBase64String)
                    .Returns(true)
                    .SetName("IsBase64String_WhenValidBase64String_ReturnsTrue");

                yield return new TestCaseData("TestString")
                    .Returns(false)
                    .SetName("IsBase64String_WhenInvalidBase64String_ReturnsFalse");

                yield return new TestCaseData(null)
                    .Returns(false)
                    .SetName("IsBase64String_WhenInputIsNull_ReturnsFalse");

                yield return new TestCaseData(string.Empty)
                    .Returns(false)
                    .SetName("IsBase64String_WhenInputIsEmpty_ReturnsFalse");

                yield return new TestCaseData(" ")
                    .Returns(false)
                    .SetName("IsBase64String_WhenInputIsWhitespace_ReturnsFalse");

                yield return new TestCaseData($" {validBase64String} ")
                    .Returns(false)
                    .SetName("IsBase64String_WhenValidBase64StringWithWhitespace_ReturnsFalse");
            }
        }
        #endregion

        #region IsEmailAddressTestCases
        public static IEnumerable<TestCaseData> IsEmailAddressTestCases
        {
            get
            {
                yield return new TestCaseData("example@example.com")
                    .Returns(true)
                    .SetName("IsEmailAddress_WhenValidEmailAddress_ReturnsTrue");

                yield return new TestCaseData("example")
                    .Returns(false)
                    .SetName("IsEmailAddress_WhenInvalidEmailAddress_ReturnsFalse");

                yield return new TestCaseData(null)
                    .Returns(false)
                    .SetName("IsEmailAddress_WhenInputIsNull_ReturnsFalse");

                yield return new TestCaseData(string.Empty)
                    .Returns(false)
                    .SetName("IsEmailAddress_WhenInputIsEmpty_ReturnsFalse");

                yield return new TestCaseData(" ")
                    .Returns(false)
                    .SetName("IsEmailAddress_WhenInputIsWhitespace_ReturnsFalse");

                yield return new TestCaseData("example@example.com ")
                    .Returns(false)
                    .SetName("IsEmailAddress_WhenValidEmailAddressWithWhitespace_ReturnsFalse");
            }
        }
        #endregion

        #region IsGuidTestCases
        public static IEnumerable<TestCaseData> IsGuidTestCases
        {
            get
            {
                yield return new TestCaseData(Guid.NewGuid().ToString())
                    .Returns(true)
                    .SetName("IsGuid_WhenValidGuid_ReturnsTrue");

                yield return new TestCaseData("invalidGuid")
                    .Returns(false)
                    .SetName("IsGuid_WhenInvalidGuid_ReturnsFalse");

                yield return new TestCaseData(null)
                    .Returns(false)
                    .SetName("IsGuid_WhenInputIsNull_ReturnsFalse");

                yield return new TestCaseData(string.Empty)
                    .Returns(false)
                    .SetName("IsGuid_WhenInputIsEmpty_ReturnsFalse");

                yield return new TestCaseData(" ")
                    .Returns(false)
                    .SetName("IsGuid_WhenInputIsWhitespace_ReturnsFalse");

                yield return new TestCaseData($" {Guid.NewGuid()} ")
                    .Returns(true)
                    .SetName("IsGuid_WhenValidGuidWithWhitespace_ReturnsTrue");
            }
        }
        #endregion

        #region IsEqualsTestCases
        public static IEnumerable<TestCaseData> IsEqualsTestCases
        {
            get
            {
                yield return new TestCaseData("test", "test", null)
                .Returns(true)
                .SetName("IsEquals_WhenStringsAreEqual_ReturnsTrue");

                yield return new TestCaseData("Test", "test", null)
                    .Returns(false)
                    .SetName("IsEquals_WhenStringsAreEqualWithDifferentCasing_ReturnsFalse");

                yield return new TestCaseData("Hello", "World", null)
                    .Returns(false)
                    .SetName("IsEquals_WhenStringsAreDifferent_ReturnsFalse");

                yield return new TestCaseData(null, "NotNull", null)
                    .Returns(false)
                    .SetName("IsEquals_WhenInputStringIsNull_ReturnsFalse");

                yield return new TestCaseData("", "NotEmpty", null)
                    .Returns(false)
                    .SetName("IsEquals_WhenInputStringIsEmpty_ReturnsFalse");

                yield return new TestCaseData("OrdinalIgnoreCaseTest", "ordinalignorecasetest", StringComparison.OrdinalIgnoreCase)
                    .Returns(true)
                    .SetName("IsEquals_WithOrdinalIgnoreCaseComparison_ReturnsTrue");

                yield return new TestCaseData("CaseInsensitive", "caseinsensitive", StringComparison.InvariantCultureIgnoreCase)
                    .Returns(true)
                    .SetName("IsEquals_WhenStringsAreEqualWithDifferentCasingAndWhenComparisonTypeInvariantCultureIgnoreCase_ReturnsTrue");
            }
        }
        #endregion

        #region IsNotNullOrEmptyTestCases
        public static IEnumerable<TestCaseData> IsNotNullOrEmptyTestCases
        {
            get
            {
                yield return new TestCaseData("TestString")
                    .Returns(true)
                    .SetName("IsNotNullOrEmpty_WhenValidStringIsProvided_ReturnsTrue");

                yield return new TestCaseData(string.Empty)
                    .Returns(false)
                    .SetName("IsNotNullOrEmpty_WhenEmptyStringIsProvided_ReturnsFalse");

                yield return new TestCaseData(null)
                    .Returns(false)
                    .SetName("IsNotNullOrEmpty_WhenNullIsProvided_ReturnsFalse");

                yield return new TestCaseData(" ")
                    .Returns(true)
                    .SetName("IsNotNullOrEmpty_WhenWhitespaceStringIsProvided_ReturnsTrue");
            }
        }
        #endregion

        #region IsNullOrEmptyTestCases
        public static IEnumerable<TestCaseData> IsNullOrEmptyTestCases
        {
            get
            {
                yield return new TestCaseData("TestString")
                    .Returns(false)
                    .SetName("IsNullOrEmpty_WhenValidStringIsProvided_ReturnsFalse");

                yield return new TestCaseData(string.Empty)
                    .Returns(true)
                    .SetName("IsNullOrEmpty_WhenEmptyStringIsProvided_ReturnsTrue");

                yield return new TestCaseData(null)
                    .Returns(true)
                    .SetName("IsNullOrEmpty_WhenNullIsProvided_ReturnsTrue");

                yield return new TestCaseData(" ")
                    .Returns(false)
                    .SetName("IsNullOrEmpty_WhenWhitespaceStringIsProvided_ReturnsFalse");
            }
        }
        #endregion

        #region IsPhoneOrFaxNumberTestCases
        public static IEnumerable<TestCaseData> IsPhoneOrFaxNumberTestCases
        {
            get
            {
                yield return new TestCaseData("123-456-7890")
                    .Returns(true)
                    .SetName("IsPhoneOrFaxNumber_WhenValidPhoneNumberIsProvided_ReturnsTrue");

                yield return new TestCaseData("987-654-3210")
                    .Returns(true)
                    .SetName("IsPhoneOrFaxNumber_WhenValidFaxNumberIsProvided_ReturnsTrue");

                yield return new TestCaseData("InvalidNumber")
                    .Returns(false)
                    .SetName("IsPhoneOrFaxNumber_WhenInvalidNumberIsProvided_ReturnsFalse");

                yield return new TestCaseData(null)
                    .Returns(false)
                    .SetName("IsPhoneOrFaxNumber_WhenNullIsProvided_ReturnsFalse");

                yield return new TestCaseData(" ")
                    .Returns(false)
                    .SetName("IsPhoneOrFaxNumber_WhenWhitespaceIsProvided_ReturnsFalse");
            }
        }
        #endregion

    }
}
