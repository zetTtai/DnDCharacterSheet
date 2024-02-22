using Services;

namespace DnDTests.Services
{
    internal class ModifierCalculatorServiceTest
    {
        private UtilsService _service;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _service = new();
        }

        [Test]
        public void ValueToAttributeModifier_GreaterThanTenEvenNumber_ReturnsString()
        {
            // Arrange
            int value = 20;
            string expected = "+5";

            // Act
            string actual = _service.ValueToAttributeModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValueToAttributeModifier_LowerThanTenEvenNumber_ReturnsString()
        {
            // Arrange
            int value = 8;
            string expected = "-1";

            // Act
            string actual = _service.ValueToAttributeModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValueToAttributeModifier_GraterThanTenOddNumber_ReturnsString()
        {
            // Arrange
            int value = 17;
            string expected = "+3";

            // Act
            string actual = _service.ValueToAttributeModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValueToAttributeModifier_LowerThanTenOddNumber_ReturnsString()
        {
            // Arrange
            int value = 1;
            string expected = "-5";

            // Act
            string actual = _service.ValueToAttributeModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValueToAttributeModifier_EqualToTen_ReturnsString()
        {
            // Arrange
            int value = 10;
            string expected = "0";

            // Act
            string actual = _service.ValueToAttributeModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
