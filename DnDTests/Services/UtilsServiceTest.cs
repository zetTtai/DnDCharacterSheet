using Enums;
using Exceptions;
using Interfaces;
using Models;
using Services;

namespace DnDTests.Services;

internal class UtilsServiceTest
{
    private IUtilsService _service;

    public static IEnumerable<CharacterAbilities> CharacterAbilitiesTestCases
    {
        get
        {
            foreach (CharacterAbilities value in Enum.GetValues(typeof(CharacterAbilities)))
            {
                yield return value;
            }
        }
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _service = new UtilsService();
    }

    [Test]
    public void ValueToAbilityModifier_GreaterThanTenEvenNumber_ReturnsString()
    {
        // Arrange
        int value = 20;
        string expected = "+5";

        // Act
        string actual = _service.ValueToAbilityModifier(value);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ValueToAbilityModifier_LowerThanTenEvenNumber_ReturnsString()
    {
        // Arrange
        int value = 8;
        string expected = "-1";

        // Act
        string actual = _service.ValueToAbilityModifier(value);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ValueToAbilityModifier_GraterThanTenOddNumber_ReturnsString()
    {
        // Arrange
        int value = 17;
        string expected = "+3";

        // Act
        string actual = _service.ValueToAbilityModifier(value);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ValueToAbilityModifier_LowerThanTenOddNumber_ReturnsString()
    {
        // Arrange
        int value = 1;
        string expected = "-5";

        // Act
        string actual = _service.ValueToAbilityModifier(value);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ValueToAbilityModifier_EqualToTen_ReturnsString()
    {
        // Arrange
        int value = 10;
        string expected = "0";

        // Act
        string actual = _service.ValueToAbilityModifier(value);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test, TestCaseSource(nameof(CharacterAbilitiesTestCases))]
    public void ModifyCapabilities_ReturnsCapabilityList(CharacterAbilities characterAbility)
    {
        // Arrange
        string value = "+2";

        List<Capability> expected = Enum.GetValues(typeof(CharacterAbilities))
            .Cast<CharacterAbilities>()
            .Select(ability => new Capability
            {
                Name = "Test_" + ability.ToString(),
                AssociatedAbility = ability,
                Value = ability == characterAbility ? value : string.Empty
            }).ToList();

        IEnumerable<Capability> capabilities = Enum.GetValues(typeof(CharacterAbilities))
            .Cast<CharacterAbilities>()
            .Select(ability => new Capability
            {
                Name = "Test_" + ability.ToString(),
                AssociatedAbility = ability,
                Value = string.Empty
            });

        // Act
        List<Capability> actual = _service.ModifyCapabilities(capabilities, value, characterAbility).ToList();

        // Assert
        Assert.That(actual, Has.Count.EqualTo(expected.Count));
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actual[i].Name, Is.EqualTo(expected[i].Name));
                Assert.That(actual[i].AssociatedAbility, Is.EqualTo(expected[i].AssociatedAbility));
                Assert.That(actual[i].Value, Is.EqualTo(expected[i].Value));
            });
        }
    }

    [Test, TestCaseSource(nameof(CharacterAbilitiesTestCases))]
    public void ModifyAbilities_ReturnsAbilityList(CharacterAbilities characterAbility)
    {
        //Arrange
        int value = 10;
        string modifier = "0";
        Sheet sheet = new();

        List<Ability> expected = Enum.GetValues(typeof(CharacterAbilities))
            .Cast<CharacterAbilities>()
            .Select(ability => new Ability
            {
                Name = ability,
                Modifier = ability == characterAbility ? modifier : string.Empty,
                Value = ability == characterAbility ? value.ToString() : string.Empty
            }).ToList();

        // Act
        List<Ability> actual = _service.ModifyAbilities(sheet.Abilities, value.ToString(), modifier, characterAbility).ToList();

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
