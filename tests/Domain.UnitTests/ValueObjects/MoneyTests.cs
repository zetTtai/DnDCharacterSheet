using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DnDCharacterSheet.Domain.UnitTests.ValueObjects;
public class MoneyTests
{
    private Money _money;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _money = new Money(1, 2, 3, 4, 5);
    }

    [TestCase(Currencies.CopperPieces, 1)]
    [TestCase(Currencies.SilverPieces, 2)]
    [TestCase(Currencies.ElectrumPieces, 3)]
    [TestCase(Currencies.GoldPieces, 4)]
    [TestCase(Currencies.PlatinumPieces, 5)]
    public void ShouldReturnQuantityByCurrency(Currencies currency, int expectedQuantity)
    {
        // Arrange
        // Act
        var quantity = _money.GetByCurrency(currency);

        // Assert
        quantity.Should().Be(expectedQuantity);
    }

    [Test]
    public void GetByCurrency_ShouldThrowInvalidOperationExceptionWhenCurrencyDoesNotExists()
    {
        // Arrange
        // Act
        try
        {
            _money.GetByCurrency((Currencies)999);
        }
        // Assert
        catch (InvalidOperationException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [TestCase(Currencies.CopperPieces)]
    [TestCase(Currencies.SilverPieces)]
    [TestCase(Currencies.ElectrumPieces)]
    [TestCase(Currencies.GoldPieces)]
    [TestCase(Currencies.PlatinumPieces)]
    public void ShouldSetQuantityByCurrency(Currencies currency)
    {
        // Arrange
        // Act
        _money.SetByCurrency(currency, 10);

        // Assert
        _money.GetByCurrency(currency).Should().Be(10);
    }

    [Test]
    public void SetByCurrency_ShouldThrowInvalidOperationExceptionWhenCurrencyDoesNotExists()
    {
        // Arrange
        // Act
        try
        {
            _money.SetByCurrency((Currencies)999, 10);
        }
        // Assert
        catch (InvalidOperationException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }
}
