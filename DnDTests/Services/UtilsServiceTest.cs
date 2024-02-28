using Enums;
using Exceptions;
using Interfaces;
using Models;
using Newtonsoft.Json.Linq;
using Services;
using Strategies;

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

        [TestCase(CharacterAttributes.STR)]
        [TestCase(CharacterAttributes.DEX)]
        [TestCase(CharacterAttributes.CON)]
        [TestCase(CharacterAttributes.INT)]
        [TestCase(CharacterAttributes.WIS)]
        [TestCase(CharacterAttributes.CHA)]
        public void ModifyCapabilities_ReturnsCapabilityList(CharacterAttributes characterAttribute)
        {
            // Arrange
            string value = "+2";

            List<Capability> expected = Enum.GetValues(typeof(CharacterAttributes))
                .Cast<CharacterAttributes>()
                .Select(attribute => new Capability
                {
                    Name = "Test_" + attribute.ToString(),
                    AssociatedAttribute = attribute,
                    Value = attribute == characterAttribute ? value : string.Empty
                }).ToList();

            IEnumerable<Capability> capabilities = Enum.GetValues(typeof(CharacterAttributes))
                .Cast<CharacterAttributes>()
                .Select(attribute => new Capability
                {
                    Name = "Test_" + attribute.ToString(),
                    AssociatedAttribute = attribute,
                    Value = string.Empty
                });

            // Act
            List<Capability> actual = _service.ModifyCapabilities(capabilities, value, characterAttribute).ToList();

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual[i].Name, Is.EqualTo(expected[i].Name));
                    Assert.That(actual[i].AssociatedAttribute, Is.EqualTo(expected[i].AssociatedAttribute));
                    Assert.That(actual[i].Value, Is.EqualTo(expected[i].Value));
                });
            }
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

            List<string> inputs = ["str", "Dex", "CON  ", "InT", "WIs", "  CHA"];
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


        [TestCase(CharacterAttributes.STR)]
        [TestCase(CharacterAttributes.DEX)]
        [TestCase(CharacterAttributes.CON)]
        [TestCase(CharacterAttributes.INT)]
        [TestCase(CharacterAttributes.WIS)]
        [TestCase(CharacterAttributes.CHA)]
        public void ModifyAttributes_ReturnsAttributeList(CharacterAttributes characterAttribute)
        {
            //Arrange
            int value = 10;
            string modifier = "0";
            Sheet sheet = new();

            List<Models.Attribute> expected = Enum.GetValues(typeof(CharacterAttributes))
                .Cast<CharacterAttributes>()
                .Select(attribute => new Models.Attribute
                {
                    Name = attribute,
                    Modifier = attribute == characterAttribute ? modifier : "",
                    Value = attribute == characterAttribute ? value.ToString() : ""
                }).ToList();

            // Act
            List<Models.Attribute> actual = _service.ModifyAttributes(sheet.Attributes, value.ToString(), modifier, characterAttribute).ToList();

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual[i].Name, Is.EqualTo(expected[i].Name));
                    Assert.That(actual[i].Modifier, Is.EqualTo(expected[i].Modifier));
                    Assert.That(actual[i].Value, Is.EqualTo(expected[i].Value));
                });
            }
        }
    }
}
