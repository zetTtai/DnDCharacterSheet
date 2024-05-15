namespace DnDCharacterSheet.Application.FunctionalTests.Currency;

using System.Net;
using DnDCharacterSheet.Application.Currency.ConvertMoney;
using DnDCharacterSheet.Domain.Enums;
using DnDCharacterSheet.Domain.ValueObjects;
using static Testing;

public class ConvertMoneyTests : BaseTestFixture
{
    private static TestCaseData CreateTestCase(
        Currencies src,
        Currencies dst,
        int quantity,
        (int initSrc, int initDst) initial,
        (int expSrc, int expDst) expected,
        string name)
    {
        return new TestCaseData(src, dst, quantity, initial, expected).SetName(name);
    }

    public static IEnumerable<TestCaseData> MoneyConversionTestCases
    {
        get
        {
            yield return CreateTestCase(Currencies.CopperPieces, Currencies.SilverPieces, 10, (10, 0), (0, 1), "TestCopperToSilverConversion");
            yield return CreateTestCase(Currencies.SilverPieces, Currencies.CopperPieces, 1, (1, 0), (0, 10), "TestSilverToCopperConversion");

            yield return CreateTestCase(Currencies.SilverPieces, Currencies.ElectrumPieces, 5, (5, 0), (0, 1), "TestSilverToElectrumConversion");
            yield return CreateTestCase(Currencies.ElectrumPieces, Currencies.SilverPieces, 1, (1, 0), (0, 5), "TestElectrumToSilverConversion");

            yield return CreateTestCase(Currencies.ElectrumPieces, Currencies.GoldPieces, 2, (2, 0), (0, 1), "TestElectrumToGoldConversion");
            yield return CreateTestCase(Currencies.GoldPieces, Currencies.ElectrumPieces, 1, (1, 0), (0, 2), "TestGoldToElectrumConversion");

            yield return CreateTestCase(Currencies.GoldPieces, Currencies.PlatinumPieces, 10, (10, 0), (0, 1), "TestGoldToPlatinumConversion");
            yield return CreateTestCase(Currencies.PlatinumPieces, Currencies.GoldPieces, 1, (1, 0), (0, 10), "TestPlatinumToGoldConversion");
        }
    }

    [TestCaseSource(nameof(MoneyConversionTestCases))]
    public async Task ShouldConvertMoney(
        Currencies src,
        Currencies dst,
        int quantity,
        (int initialSrcQuantity, int initialDstQuantity) initialQuantities,
        (int expectedSrcQuantity, int expectedDstQuantity) expectedQuantities)
    {
        // Arrange
        var currentMoney = new Money();
        currentMoney.SetByCurrency(src, initialQuantities.initialSrcQuantity);
        currentMoney.SetByCurrency(dst, initialQuantities.initialDstQuantity);

        var command = new ConvertMoneyCommand()
        {
            CurrentMoney = currentMoney,
            SrcCurrency = src,
            DstCurrency = dst,
            Quantity = quantity
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var money = response.Value;
        if (money is null)
        {
            Assert.Fail();
            return;
        }
        money.GetByCurrency(src).Should().Be(expectedQuantities.expectedSrcQuantity);
        money.GetByCurrency(dst).Should().Be(expectedQuantities.expectedDstQuantity);
    }

    [Test]
    public async Task IfConversionNotHandled_ReturnResponseWith400()
    {
        // Arrange
        var currentMoney = new Money();

        var command = new ConvertMoneyCommand()
        {
            CurrentMoney = currentMoney,
            SrcCurrency = Currencies.CopperPieces,
            DstCurrency = Currencies.GoldPieces,
            Quantity = 10
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();  
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task IfSrcCurrencyIsNotValid_ReturnResponseWith400()
    {
        // Arrange
        var currentMoney = new Money();

        var command = new ConvertMoneyCommand()
        {
            CurrentMoney = currentMoney,
            SrcCurrency = (Currencies) 999,
            DstCurrency = Currencies.GoldPieces,
            Quantity = 10
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task IfDstCurrencyIsNotValid_ReturnResponseWith400()
    {
        // Arrange
        var currentMoney = new Money(goldPieces: 10);

        var command = new ConvertMoneyCommand()
        {
            CurrentMoney = currentMoney,
            SrcCurrency = Currencies.GoldPieces,
            DstCurrency = (Currencies)999,
            Quantity = 10
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task IfInvalidQuantity_ReturnResponseWith400()
    {
        // Arrange
        var currentMoney = new Money();

        var command = new ConvertMoneyCommand()
        {
            CurrentMoney = currentMoney,
            SrcCurrency = Currencies.GoldPieces,
            DstCurrency = Currencies.ElectrumPieces,
            Quantity = 10
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Test]
    public async Task IfQuantityIsLowerOrEqualToZero_ReturnResponseWith400()
    {
        // Arrange
        var currentMoney = new Money();

        var command = new ConvertMoneyCommand()
        {
            CurrentMoney = currentMoney,
            SrcCurrency = Currencies.GoldPieces,
            DstCurrency = Currencies.ElectrumPieces,
            Quantity = 0
        };

        // Act
        var response = await SendAsync(command);

        // Assert
        response.Succeeded.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
