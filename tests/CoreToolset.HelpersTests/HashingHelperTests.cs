using CoreToolset.Helpers;

namespace CoreToolset.HelpersTests
{
    [TestFixture]
    public class HashingHelperTests
    {
        [Test]
        public void CreatePasswordHash_ValidInput_ReturnsValidHashAndSalt()
        {
            string password = "TestPassword";
            HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            Assert.Multiple(() =>
            {
                Assert.That(passwordHash, Is.Not.Null);
                Assert.That(passwordSalt, Is.Not.Null);
                Assert.That(passwordHash, Is.Not.Empty);
                Assert.That(passwordSalt, Is.Not.Empty);
            });
        }

        [Test]
        public void VerifyPasswordHash_ValidPassword_ReturnsTrue()
        {
            string password = "TestPassword";
            HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            bool result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);

            Assert.That(result, Is.True);
        }

        [Test]
        public void VerifyPasswordHash_InvalidPassword_ReturnsFalse()
        {
            string password = "TestPassword";
            HashingHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            bool result = HashingHelper.VerifyPasswordHash("InvalidPassword", passwordHash, passwordSalt);
            Assert.That(result, Is.False);
        }

    }
}