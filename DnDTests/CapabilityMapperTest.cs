using DTOs;
using Enums;
using Interfaces;
using Mappers;
using Models;
using Services;

namespace DnDTests
{
    public class CapabilityMapperTest
    {
        [SetUp]
        public void SetUp()
        {

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
            IConverter<Capability, CapabilityDTO> mapper = new CapabilityConverter();
            CapabilityDTO actual = mapper.Convert(capability);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(expected.Id));
                Assert.That(actual.AssociatedAttribute, Is.EqualTo(expected.AssociatedAttribute));
                Assert.That(actual.Value, Is.EqualTo(expected.Value));
            });
        }

        [Test]
        public void Convert_EmptyList_ReturnCapabilityDTOList()
        {
            // Arrange
            IEnumerable<Capability> capabilities = new List<Capability>();

            // Act
            IConverter<Capability, CapabilityDTO> mapper = new CapabilityConverter();
            List<CapabilityDTO> actual = (List<CapabilityDTO>)mapper.Convert(capabilities);

            // Assert
            Assert.That(actual, Is.Empty);
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
            IConverter<Capability, CapabilityDTO> mapper = new CapabilityConverter();
            List<CapabilityDTO> actual = (List<CapabilityDTO>)mapper.Convert(capabilities);

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
