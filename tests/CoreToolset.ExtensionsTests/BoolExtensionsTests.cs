namespace CoreToolset.ExtensionsTests
{
    [TestFixture]
    public class BoolExtensionsTests
    {
        [Test]
        public void IsFalse_ShouldReturnTrue_WhenInputIsFalse()
        {
            bool input = false;
            bool result = input.IsFalse();
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsFalse_ShouldReturnFalse_WhenInputIsTrue()
        {
            bool input = true;
            bool result = input.IsFalse();
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsTrue_ShouldReturnTrue_WhenInputIsTrue()
        {
            bool input = true;
            bool result = input.IsTrue();
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsTrue_ShouldReturnFalse_WhenInputIsFalse()
        {
            bool input = false;
            bool result = input.IsTrue();
            Assert.That(result, Is.False);
        }
    }
}