using Enums;
using Factories;
using Interfaces;
using Moq;
using Strategies;

namespace DnDTests.Factories
{
    internal class SettingAttributeStrategyFactoryTest
    {
        private Mock<IUtilsService> _utilsServiceMock;
        private ISettingAttributeStrategyFactory _factory;
        private readonly string _expectedInvalidMethodError = "Method must be 0 => RollingDice, 1 => PointBuy or 2 => StandardArray";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _utilsServiceMock = new Mock<IUtilsService>();
            _factory = new SettingAttributeStrategyFactory(_utilsServiceMock.Object);
        }

        [Test]
        public void CreateStrategy_RollingDice_ReturnsRollingDiceStrategy()
        {
            // Arrange
            // Act
            IAttributeSettingStrategy actual = _factory.CreateStrategy(0);

            // Assert
            Assert.That(actual, Is.InstanceOf<RollingDiceStrategy>());
        }

        [Test]
        public void CreateStrategy_PointBuy_ReturnsRollingDiceStrategy()
        {
            // Arrange
            // Act
            IAttributeSettingStrategy actual = _factory.CreateStrategy((MethodsToIncreaseAttributes)1);

            // Assert
            Assert.That(actual, Is.InstanceOf<PointBuyStrategy>());
        }

        [Test]
        public void CreateStrategy_StandardArray_ReturnsRollingDiceStrategy()
        {
            // Arrange
            // Act
            IAttributeSettingStrategy actual = _factory.CreateStrategy((MethodsToIncreaseAttributes)2);

            // Assert
            Assert.That(actual, Is.InstanceOf<StandardArrayStrategy>());
        }

        [Test]
        public void CreateStrategy_InvalidMethod_ReturnsException()
        {
            // Arrange
            string actual = "";
            
            // Act
            try
            {
                _ = _factory.CreateStrategy((MethodsToIncreaseAttributes)3);
            } catch (Exception ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(_expectedInvalidMethodError));
        }
    }
}
