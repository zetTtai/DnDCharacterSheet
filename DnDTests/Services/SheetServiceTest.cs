using DnDCharacterSheet;
using Enums;
using Interfaces;
using Models;
using Moq;
using Services;
using Strategies;

namespace DnDTests.Services
{
    internal class SheetServiceTest
    {
        private SheetService _service;
        private Mock<IAttributeSettingStrategy> _attributeSettingStrategyMock;
        private readonly string _strategyNotSetErrorMessage = "Strategy was not set";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _attributeSettingStrategyMock = new Mock<IAttributeSettingStrategy>();
            _attributeSettingStrategyMock
                .Setup(m => m.SetStrengthAttribute(It.IsAny<Sheet>(), It.IsAny<int>()))
                .Returns(new Sheet());
            _service = new();
        }

        [Test]
        public void SetStrengthAttribute_RollingDice_ModifyStrengthAttribute_ReturnsSheet()
        {
            // Arrange
            _service.SetStrategy(_attributeSettingStrategyMock.Object);
            Sheet expected = new();

            // Act
            Sheet actual = _service.SetStrengthAttribute(new Sheet(), 3);

            // Assert
            Assert.That(actual.StrengthAttribute, Is.EqualTo(expected.StrengthAttribute));
        }

        [Test]
        public void SetStrengthAttribute_NoStrategy_ReturnsException()
        {
            // Arrange
            string actual = "";

            // Act
            try
            {
                _ = _service.SetStrengthAttribute(new Sheet(), 3);
            }
            catch (Exception ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(_strategyNotSetErrorMessage));
        }
    }
}
