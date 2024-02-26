using Enums;
using Exceptions;
using Interfaces;
using Models;
using Services;

namespace DnDTests.Services
{
    internal class UtilsServiceTest
    {
        private IUtilsService _service;
        private readonly string _invalidAttributeError = "Attribute must be STR, DEX, CON, INT, WIS or CHA";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _service = new UtilsService();
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

        [Test]
        public void ModifyCapabilities_ReturnsCapabilityList()
        {
            // Arrange
            IEnumerable<Capability> capabilities = [
                new()
                {
                    Name = "Test1",
                    AssociatedAttribute = CharacterAttributes.STR,
                    Value = "",
                },
                new()
                {
                    Name = "Test1",
                    AssociatedAttribute = CharacterAttributes.DEX,
                    Value = "",
                }
            ];

            List<Capability> expected = [
                new()
                {
                    Name = "Test1",
                    AssociatedAttribute = CharacterAttributes.STR,
                    Value = "+2",
                },
                new()
                {
                    Name = "Test1",
                    AssociatedAttribute = CharacterAttributes.DEX,
                    Value = "",
                }
            ];

            // Act
            List<Capability> actual = _service.ModifyCapabilities(capabilities, "+2", CharacterAttributes.STR).ToList();

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));

            Assert.Multiple(() =>
            {
                for(int i = 0; i < expected.Count; i++)
                {
                    Assert.That(actual[i].Name, Is.EqualTo(expected[i].Name));
                    Assert.That(actual[i].AssociatedAttribute, Is.EqualTo(expected[i].AssociatedAttribute));
                    Assert.That(actual[i].Value, Is.EqualTo(expected[i].Value));
                }
            });
        }

        [Test]
        public void StringToCharacterAttributes_ReturnsCharacterAttribute()
        {
            // Arrange
            List<CharacterAttributes> expected = [
                CharacterAttributes.STR,
                CharacterAttributes.DEX,
                CharacterAttributes.CON,
                CharacterAttributes.INT,
                CharacterAttributes.WIS,
                CharacterAttributes.CHA
            ];

            List<string> inputs = ["str", "Dex", "CON", "InT", "WIs", "CHA"];
            List<CharacterAttributes> actual = [];

            // Act
            foreach (string input in inputs)
            {
                actual.Add(_service.StringToCharacterAttribute(input));
            }

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));

            for (int i =0; i < expected.Count;i++)
            {
                Assert.That(actual[i], Is.EqualTo(expected[i]));
            }
        }

        [Test]
        public void StringToCharacterAttributes_InvalidCharacterAttribute_ReturnsException()
        {
            // Arrange
            string actual = "";

            // Act
            try
            {
                _service.StringToCharacterAttribute("EXE");
            }
            catch (BadRequestException ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(_invalidAttributeError));
        }
    }
}
