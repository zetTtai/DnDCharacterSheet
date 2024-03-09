using Enums;
using Exceptions;
using Interfaces;
using Models;
using Services;

namespace DnDTests.Services;

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
        string actual = _service.ValueToAbilityModifier(value);

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
        string actual = _service.ValueToAbilityModifier(value);

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
        string actual = _service.ValueToAbilityModifier(value);

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
        string actual = _service.ValueToAbilityModifier(value);

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
        string actual = _service.ValueToAbilityModifier(value);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase(CharacterAbilities.STR)]
    [TestCase(CharacterAbilities.DEX)]
    [TestCase(CharacterAbilities.CON)]
    [TestCase(CharacterAbilities.INT)]
    [TestCase(CharacterAbilities.WIS)]
    [TestCase(CharacterAbilities.CHA)]
    public void ModifyCapabilities_ReturnsCapabilityList(CharacterAbilities characterAttribute)
    {
        // Arrange
        string value = "+2";

        List<Capability> expected = Enum.GetValues(typeof(CharacterAbilities))
            .Cast<CharacterAbilities>()
            .Select(attribute => new Capability
            {
                Name = "Test_" + attribute.ToString(),
                AssociatedAttribute = attribute,
                Value = attribute == characterAttribute ? value : string.Empty
            }).ToList();

        IEnumerable<Capability> capabilities = Enum.GetValues(typeof(CharacterAbilities))
            .Cast<CharacterAbilities>()
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
        List<CharacterAbilities> expected = [
            CharacterAbilities.STR,
            CharacterAbilities.DEX,
            CharacterAbilities.CON,
            CharacterAbilities.INT,
            CharacterAbilities.WIS,
            CharacterAbilities.CHA
        ];

        List<string> inputs = ["str", "Dex", "CON  ", "InT", "WIs", "  CHA"];
        List<CharacterAbilities> actual = [];

        // Act
        foreach (string input in inputs)
        {
            actual.Add(_service.StringToCharacterAbility(input));
        }

        // Assert
        Assert.That(actual, Has.Count.EqualTo(expected.Count));

        for (int i = 0; i < expected.Count; i++)
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
            _ = _service.StringToCharacterAbility("EXE");
        }
        catch (BadRequestException ex)
        {
            actual = ex.Message;
        }

        // Assert
        Assert.That(actual, Is.EqualTo(_invalidAttributeError));
    }


    [TestCase(CharacterAbilities.STR)]
    [TestCase(CharacterAbilities.DEX)]
    [TestCase(CharacterAbilities.CON)]
    [TestCase(CharacterAbilities.INT)]
    [TestCase(CharacterAbilities.WIS)]
    [TestCase(CharacterAbilities.CHA)]
    public void ModifyAttributes_ReturnsAttributeList(CharacterAbilities characterAttribute)
    {
        //Arrange
        int value = 10;
        string modifier = "0";
        Sheet sheet = new();

        List<Models.Ability> expected = Enum.GetValues(typeof(CharacterAbilities))
            .Cast<CharacterAbilities>()
            .Select(attribute => new Models.Ability
            {
                Name = attribute,
                Modifier = attribute == characterAttribute ? modifier : "",
                Value = attribute == characterAttribute ? value.ToString() : ""
            }).ToList();

        // Act
        List<Models.Ability> actual = _service.ModifyAbility(sheet.Attributes, value.ToString(), modifier, characterAttribute).ToList();

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
