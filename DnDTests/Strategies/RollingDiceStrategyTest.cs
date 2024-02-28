using Enums;
using Exceptions;
using Interfaces;
using Models;
using Moq;
using Strategies;

namespace DnDTests.Strategies
{
    internal class RollingDiceStrategyTest
    {
        private readonly string _expectedModifier = "+4";
        private readonly string _expectedInvalidValueErrorMessage = "Value must be between 3 and 18";
        private Mock<IUtilsService> _utilsServiceMock;
        private RollingDiceStrategy _strategy;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _utilsServiceMock = new Mock<IUtilsService>();
            _utilsServiceMock
                .Setup(m => m.ValueToAttributeModifier(It.IsAny<int>()))
                .Returns(_expectedModifier);
            _utilsServiceMock
                .Setup(m => m.ModifyCapabilities(It.IsAny<IEnumerable<Capability>>(), It.IsAny<string>(), It.IsAny<CharacterAttributes>()))
                .Returns([]);
            _utilsServiceMock
                .Setup(m => m.ModifyAttributes(It.IsAny<IEnumerable<Models.Attribute>>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CharacterAttributes>()))
                .Returns([]);
            _strategy = new RollingDiceStrategy(_utilsServiceMock.Object, CharacterAttributes.STR);
        }

        [Test]
        public void SetStrengthAttribute_ValidValue_ReturnsSheet()
        {
            // Arrange
            // Act
            try
            {
                _ = _strategy.SetAttribute(new Sheet(), 8);
            }
            catch (BadRequestException)
            {
                Assert.Fail();
            }

            // Assert
            Assert.Pass();
        }

        [Test]
        public void SetStrengthAttribute_ValueLowerThan3_ReturnsException()
        {
            // Arrange
            string actual = "";

            // Act
            try
            {
                _ = _strategy.SetAttribute(new Sheet(), 2);
            }
            catch (BadRequestException ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(_expectedInvalidValueErrorMessage));
        }

        [Test]
        public void SetStrengthAttribute_ValueHigherThan18_ReturnsException()
        {
            // Arrange
            string actual = "";

            // Act
            try
            {
                _ = _strategy.SetAttribute(new Sheet(), 19);
            }
            catch (BadRequestException ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(_expectedInvalidValueErrorMessage));
        }
    }
}
