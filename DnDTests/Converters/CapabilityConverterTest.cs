using Converters;
using DTOs;
using Enums;
using Interfaces;
using Models;

namespace DnDTests.Converters
{
    internal class CapabilityConverterTest
    {
        private IConverter<Capability, CapabilityDTO> _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = new CapabilityConverter();
        }
        [Test]
        public void Convert_Capability_ReturnCapabilityDTO()
        {
            // Arrange
            Capability capability = new()
            {
                Name = "Test",
                AssociatedAttribute = CharacterAttributes.STR,
                Value = "Test",
            };

            CapabilityDTO expected = new()
            {
                Id = "Test",
                AssociatedAttribute = "STR",
                Value = "Test",
            };

            // Act
            CapabilityDTO actual = _mapper.Convert(capability);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(expected.Id));
                Assert.That(actual.AssociatedAttribute, Is.EqualTo(expected.AssociatedAttribute));
                Assert.That(actual.Value, Is.EqualTo(expected.Value));
            });
        }

        [Test]
        public void Convert_List_ReturnCapabilityDTOList()
        {
            // Arrange
            IEnumerable<Capability> capabilities = [
                new Capability()
                {
                    Name = "Athletics",
                    AssociatedAttribute = CharacterAttributes.STR,
                    Value = string.Empty,
                },
                new Capability()
                {
                    Name = "Acrobatics",
                    AssociatedAttribute = CharacterAttributes.DEX,
                    Value = string.Empty,
                },
            ];
            List<CapabilityDTO> expected = [
                new CapabilityDTO()
                {
                    Id = "Athletics",
                    AssociatedAttribute = "STR",
                    Value = string.Empty,
                },
                new CapabilityDTO()
                {
                    Id = "Acrobatics",
                    AssociatedAttribute = "DEX",
                    Value = string.Empty,
                }
            ];

            // Act
            List<CapabilityDTO> actual = (List<CapabilityDTO>)_mapper.Convert(capabilities);

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
                    Assert.That(actual[i].AssociatedAttribute, Is.EqualTo(expected[i].AssociatedAttribute));
                    Assert.That(actual[i].Value, Is.EqualTo(expected[i].Value));
                });
            }
        }
    }
}
