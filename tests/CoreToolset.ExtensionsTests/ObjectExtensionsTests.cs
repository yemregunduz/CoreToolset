namespace CoreToolset.ExtensionsTests
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [Test]
        public void IsNull_ShouldReturnTrue_WhenInputIsNull()
        {
            object? input = null;
            bool result = input.IsNull();
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNull_ShouldReturnFalse_WhenInputIsNotNull()
        {
            object input = new();
            bool result = input.IsNull();
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsNotNull_ShouldReturnTrue_WhenInputIsNotNull()
        {
            object input = new();
            bool result = input.IsNotNull();
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsNotNull_ShouldReturnFalse_WhenInputIsNull()
        {
            object? input = null;
            bool result = input.IsNotNull();
            Assert.That(result, Is.False);
        }

        [Test]
        public void Is_ShouldReturnTrue_WhenValueIsNotNullAndFuncReturnsTrue()
        {
            object value = new();
            bool result = value.Is(() => true);
            Assert.That(result, Is.True);
        }

        [Test]
        public void Is_ShouldReturnFalse_WhenValueIsNotNullAndFuncReturnsFalse()
        {
            object value = new();
            bool result = value.Is(() => false);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Is_ShouldReturnFalse_WhenValueIsNull()
        {
            object value = null;
            bool result = value.Is(() => true);
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsNot_ShouldReturnTrue_WhenValueIsNotNullAndFuncReturnsFalse()
        {
            object value = new();
            Func<bool> func = () => false;
            bool result = value.IsNot(func);
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsNot_ShouldReturnFalse_WhenValueIsNotNullAndFuncReturnsTrue()
        {
            object value = new();
            bool result = value.IsNot(() => true);
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsNot_ShouldReturnTrue_WhenValueIsNull()
        {
            object value = null;
            bool result = value.IsNot(() => false);
            Assert.That(result, Is.True);
        }
    }
}
