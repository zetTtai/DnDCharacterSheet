using Converters;
using DTOs;
using Enums;
using Interfaces;
using Models;

namespace DnDTests.Converters;

internal class AttributeConverterTest
{
    private IConverter<Ability, AttributeDTO> _mapper;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _mapper = new AttributeConverter();
    }

    [Test]
    public void Convert_Attribute_ReturnAttributeDTO()
    {
        // Arrange
        Ability attribute = new()
        {
            Name = CharacterAbilities.STR,
            Modifier = "+2",
            Value = "12",
        };

        AttributeDTO expected = new()
        {
            Name = "STR",
            Modifier = "+2",
            Value = "12",
        };

        // Act
        AttributeDTO actual = _mapper.Convert(attribute);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Modifier, Is.EqualTo(expected.Modifier));
            Assert.That(actual.Value, Is.EqualTo(expected.Value));
        });
    }

    [Test]
    public void Convert_List_ReturnAttributeDTOList()
    {
        // Arrange
        IEnumerable<Ability> attributes = [
            new()
            {
                Name = CharacterAbilities.STR,
                Modifier = "+2",
                Value = "12",
            },
            new()
            {
                Name = CharacterAbilities.DEX,
                Modifier = "0",
                Value = "10",
            },
        ];
        List<AttributeDTO> expected = [
            new()
            {
                Name = "STR",
                Modifier = "+2",
                Value = "12",
            },
            new()
            {
                Name = "DEX",
                Modifier = "0",
                Value = "10",
            },
        ];

        // Act
        List<AttributeDTO> actual = _mapper.Convert(attributes).ToList();

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
