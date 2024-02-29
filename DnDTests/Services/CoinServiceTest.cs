using DTOs;
using Interfaces;
using Models;
using Moq;
using Services;

namespace DnDTests.Services
{
    internal class CoinServiceTest
    {
        private CoinService _service;
        private Mock<ICoinRepository> _coinRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _coinRepository = new Mock<ICoinRepository>();
            _coinRepository.Setup(m => m.GetAllCoins()).Returns(new List<Coin>()
            {
                new() { Id = 1, Name = "Gold Piece", Initials = "GP" },
                new() { Id = 2, Name = "Silver Piece", Initials = "SP" }
            });

            _service = new CoinService(_coinRepository.Object);
        }

        [Test]
        public void GetCoins_ReturnsCoinDTOList()
        {
            // Arrange
            // Act
            List<CoinDTO> actual = _service.GetAllCoins().ToList();

            // Assert
            _coinRepository.Verify(repo => repo.GetAllCoins(), Times.Once);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual, Has.Count.EqualTo(2));
            
            Assert.Multiple(() =>
            {
                Assert.That(actual[0].Name, Is.EqualTo("Gold Piece"));
                Assert.That(actual[0].Initials, Is.EqualTo("GP"));

                Assert.That(actual[1].Name, Is.EqualTo("Silver Piece"));
                Assert.That(actual[1].Initials, Is.EqualTo("SP"));
            });
        }
    }
}
