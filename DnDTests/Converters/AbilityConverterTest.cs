using Converters;
using DTOs;
using Enums;
using Interfaces;
using Models;

namespace DnDTests.Converters;

internal class AbilityConverterTest
{
    private IConverter<Ability, AbilityDTO> _mapper;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _mapper = new AbilityConverter();
    }

    [Test]
    public void Convert_Ability_ReturnAbilityDTO()
    {
        // Arrange
        Ability ability = new()
        {
            Name = CharacterAbilities.STR,
            Modifier = "+2",
            Value = "12",
        };

        AbilityDTO expected = new("STR", "12", "+2");

        // Act
        AbilityDTO actual = _mapper.Convert(ability);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Modifier, Is.EqualTo(expected.Modifier));
            Assert.That(actual.Value, Is.EqualTo(expected.Value));
        });
    }

    [Test]
    public void Convert_List_ReturnAbilityDTOList()
    {
        // Arrange
        IEnumerable<Ability> abilities = [
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
        List<AbilityDTO> expected = [
            new("STR", "12", "+2"),
            new("DEX", "10", "0")
        ];

        // Act
        List<AbilityDTO> actual = _mapper.Convert(abilities).ToList();

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
