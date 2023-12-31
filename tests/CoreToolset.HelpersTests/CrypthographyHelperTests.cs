using CoreToolset.Helpers;
using CoreToolset.Helpers.Options;
using System.Security.Cryptography;


namespace CoreToolset.HelpersTests
{
    [TestFixture]
    public class CryptographyHelperTests
    {
        [Test]
        public void Constructor_Should_Throw_Exception_On_Null_Options()
        {
            CryptographyHelperOptions options = null;
            Assert.Throws<ArgumentNullException>(() => new CryptographyHelper(options));
        }

        [Test]
        public void EncryptAndDecrypt_ValidPlainText_MatchesOriginal()
        {
            string plainText = "This is a test message";
            CryptographyHelper cryptographyHelper = new();
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            string decryptedText = cryptographyHelper.Decrypt(encryptedText);
            Assert.That(decryptedText, Is.EqualTo(plainText));
        }

        [Test]
        public void VerifyEncryptedText_ValidPlainTextAndEncryptedText_ReturnsTrue()
        {
            string plainText = "This is a test message";
            CryptographyHelper cryptographyHelper = new();
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            bool result = cryptographyHelper.VerifyEncyrptedText(plainText, encryptedText);
            Assert.That(result, Is.True);
        }

        [Test]
        public void VerifyEncryptedText_InvalidPlainText_ReturnsFalse()
        {
            string plainText = "This is a test message";
            CryptographyHelper cryptographyHelper = new();
            string encryptedText = cryptographyHelper.Encrypt(plainText);
            bool result = cryptographyHelper.VerifyEncyrptedText("InvalidPlainText", encryptedText);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Decrypt_NullEncryptedText_ThrowsException()
        {
            CryptographyHelper cryptographyHelper = new();
            Assert.Throws<ArgumentNullException>(() => cryptographyHelper.Decrypt(null));
        }
    }
}