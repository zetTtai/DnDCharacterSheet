using DnDCharacterSheet;
using Interfaces;
using Models;
using Moq;
using Services;

namespace DnDTests.Services
{
    internal class SheetServiceTest
    {
        private Mock<IUtilsService> _modifierCalculatorMock;
        private readonly string _expectedModifier = "+4";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _modifierCalculatorMock = new Mock<IUtilsService>();
            _modifierCalculatorMock.Setup(m => m.ValueToAttributeModifier(It.IsAny<int>())).Returns(_expectedModifier);
            SheetService service = new(_modifierCalculatorMock.Object);
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ModifyStrengthScore_ReturnsSheet()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            Sheet expected = new()
            {
                StrengthScore = _expectedModifier
            };

            // Act
            Sheet actual = service.SetStrengthAttribute(new Sheet(), 3);

            // Assert
            Assert.That(actual.StrengthScore, Is.EqualTo(expected.StrengthScore));
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ModifySkills_ReturnsSheet()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            Sheet expected = new()
            {
                StrengthScore = _expectedModifier
            };
            List<Capability> expectedSkills = [.. expected.Skills];
            expectedSkills[0].Value = _expectedModifier;

            // Act
            Sheet actual = service.SetStrengthAttribute(new Sheet(), 3);
            List<Capability> actualSkills = [.. actual.Skills];

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.StrengthScore, Is.EqualTo(expected.StrengthScore));
                Assert.That(actualSkills, Has.Count.EqualTo(expectedSkills.Count));
            });
            for (int i = 0; i < expectedSkills.Count; i++)
            {
                Assert.That(actualSkills[i].Value, Is.EqualTo(expectedSkills[i].Value));
            }
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ModifySavingThrows_ReturnsSheet()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            Sheet expected = new()
            {
                StrengthScore = _expectedModifier
            };
            List<Capability> expectedSavingThrows = [.. expected.SavingThrows];
            expectedSavingThrows[0].Value = _expectedModifier;

            // Act
            Sheet actual = service.SetStrengthAttribute(new Sheet(), 18);
            List<Capability> actualSavingThrows = [.. actual.SavingThrows];

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.StrengthScore, Is.EqualTo(expected.StrengthScore));
                Assert.That(actualSavingThrows, Has.Count.EqualTo(expectedSavingThrows.Count));
            });

            for (int i = 0; i < expectedSavingThrows.Count; i++)
            {
                Assert.That(actualSavingThrows[i].Value, Is.EqualTo(expectedSavingThrows[i].Value));
            }
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ValueLowerThanMin_ReturnsException()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            string expected = "Invalid value, must be between "
                + Constants.MethodsToIncreaseAttributes.RollingDice.Min
                + " and " + Constants.MethodsToIncreaseAttributes.RollingDice.Max;
            string actual = "";
            // Act
            try
            {
                Sheet sheet = service.SetStrengthAttribute(new Sheet(),
                    Constants.MethodsToIncreaseAttributes.RollingDice.Min - 1);
            }
            catch (Exception ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ValueHigherThanMax_ReturnsException()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            string expected = "Invalid value, must be between "
                + Constants.MethodsToIncreaseAttributes.RollingDice.Min
                + " and " + Constants.MethodsToIncreaseAttributes.RollingDice.Max;
            string actual = "";
            // Act
            try
            {
                Sheet sheet = service.SetStrengthAttribute(new Sheet(),
                    Constants.MethodsToIncreaseAttributes.RollingDice.Max + 1);
            }
            catch (Exception ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
