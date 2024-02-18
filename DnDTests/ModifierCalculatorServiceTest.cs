using Services;

namespace DnDTests
{
    internal class ModifierCalculatorServiceTest
    {
        [Test]
        public void ValueToModifier_GreaterThanTenEvenNumber_ReturnsString()
        {
            // Arrange
            int value = 20;
            string expected = "+5";
            ModifierCalculatorService service = new();

            // Act
            string actual = service.ValueToModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValueToModifier_LowerThanTenEvenNumber_ReturnsString()
        {
            // Arrange
            int value = 8;
            string expected = "-1";
            ModifierCalculatorService service = new();

            // Act
            string actual = service.ValueToModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValueToModifier_GraterThanTenOddNumber_ReturnsString()
        {
            // Arrange
            int value = 17;
            string expected = "+3";
            ModifierCalculatorService service = new();

            // Act
            string actual = service.ValueToModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValueToModifier_LowerThanTenOddNumber_ReturnsString()
        {
            // Arrange
            int value = 1;
            string expected = "-5";
            ModifierCalculatorService service = new();

            // Act
            string actual = service.ValueToModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ValueToModifier_EqualToTen_ReturnsString()
        {
            // Arrange
            int value = 10;
            string expected = "0";
            ModifierCalculatorService service = new();

            // Act
            string actual = service.ValueToModifier(value);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
