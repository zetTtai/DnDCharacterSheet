using Converters;
using DTOs;
using Enums;
using Interfaces;
using Models;

namespace DnDTests.Converters;

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
            AssociatedAbility = CharacterAbilities.STR,
            Value = "Test",
        };

        CapabilityDTO expected = new("Test", "STR", "Test");

        // Act
        CapabilityDTO actual = _mapper.Convert(capability);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.AssociatedAbility, Is.EqualTo(expected.AssociatedAbility));
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
                AssociatedAbility = CharacterAbilities.STR,
                Value = string.Empty,
            },
            new Capability()
            {
                Name = "Acrobatics",
                AssociatedAbility = CharacterAbilities.DEX,
                Value = string.Empty,
            },
        ];
        List<CapabilityDTO> expected = [
            new CapabilityDTO("Athletics", "STR", string.Empty),
            new CapabilityDTO("Acrobatics", "DEX", string.Empty)
        ];

        // Act
        List<CapabilityDTO> actual = _mapper.Convert(capabilities).ToList();

        // Assert
        Assert.That(actual, Has.Count.EqualTo(expected.Count));
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
                Assert.That(actual[i].AssociatedAbility, Is.EqualTo(expected[i].AssociatedAbility));
                Assert.That(actual[i].Value, Is.EqualTo(expected[i].Value));
            });
        }
    }
}
