using DnDCharacterSheet.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DnDCharacterSheet.Domain.UnitTests.ValueObjects;
public class MoneyTests
{
    [TestCase(10, 10, 0, 1)] // Happy path
    [TestCase(15, 15, 5, 1)] // Not multiple of 10
    [TestCase(12, 20, 10, 1)] // Lower than total amount
    [TestCase(0, 0, 0, 0)] // Edge case
    public void ShouldConvertCopperToSilver(int quantity, int initialCopper, int expectedCopper, int expectedSilver)
    {
        // Arrange
        var money = new Money(copperPieces: initialCopper);

        // Act
        money.ConvertCopperToSilver(quantity);

        // Assert
        money.CopperPieces.Should().Be(expectedCopper);
        money.SilverPieces.Should().Be(expectedSilver);
    }

    [TestCase(5, 5, 0, 1)] // Happy path
    [TestCase(12, 12, 2, 2)] // Not multiple of 5
    [TestCase(22, 30, 10, 4)] // Lower than total amount
    [TestCase(0, 0, 0, 0)] // Edge case
    public void ShouldConvertSilverToElectrum(int quantity, int initialSilver, int expectedSilver, int expectedElectrum)
    {
        // Arrange
        var money = new Money(silverPieces: initialSilver);

        // Act
        money.ConvertSilverToElectrum(quantity);

        // Assert
        money.SilverPieces.Should().Be(expectedSilver);
        money.ElectrumPieces.Should().Be(expectedElectrum);
    }

    [TestCase(2, 2, 0, 1)] // Happy path
    [TestCase(5, 5, 1, 2)] // Not multiple of 2
    [TestCase(25, 30, 6, 12)] // Lower than total amount
    [TestCase(0, 0, 0, 0)] // Edge case
    public void ShouldConvertElectrumToGold(int quantity, int initialElectrum, int expectedElectrum, int expectedGold)
    {
        // Arrange
        var money = new Money(electrumPieces: initialElectrum);

        // Act
        money.ConvertElectrumToGold(quantity);

        // Assert
        money.ElectrumPieces.Should().Be(expectedElectrum);
        money.GoldPieces.Should().Be(expectedGold);
    }

    [TestCase(10, 10, 0, 1)] // Happy path
    [TestCase(15, 15, 5, 1)] // Not multiple of 10
    [TestCase(12, 20, 10, 1)] // Lower than total amount
    [TestCase(0, 0, 0, 0)] // Edge case
    public void ShouldConvertGoldToPlatinum(int quantity, int initialGold, int expectedGold, int expectedPlatinum)
    {
        // Arrange
        var money = new Money(goldPieces: initialGold);

        // Act
        money.ConvertGoldToPlatinum(quantity);

        // Assert
        money.GoldPieces.Should().Be(expectedGold);
        money.PlatinumPieces.Should().Be(expectedPlatinum);
    }

}
