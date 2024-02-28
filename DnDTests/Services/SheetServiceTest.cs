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
                .Setup(m => m.SetAttribute(It.IsAny<Sheet>(), It.IsAny<int>()))
                .Returns(new Sheet());
            _service = new();
        }

        [Test]
        public void SetAttribute_StrategySet_ReturnsSheet()
        {
            // Arrange
            _service.SetAttributeSettingStrategy(_attributeSettingStrategyMock.Object);

            // Act
            try
            {
                _service.SetAttribute(new Sheet(), 3);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            // Assert
            Assert.Pass();
        }

        [Test]
        public void SetAttribute_NoStrategy_ReturnsException()
        {
            // Arrange
            string actual = "";

            // Act
            try
            {
                _service.SetAttribute(new Sheet(), 3);
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
