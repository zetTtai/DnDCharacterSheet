using DTOs;
using Exceptions;
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
        private Mock<IConverter<Coin, CoinDTO>> _converter;
        private Coin _expectedFirstCoin;
        private Coin _expectedSecondCoin;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _expectedFirstCoin = new() { Id = 1, Name = "Gold Piece", Initials = "GP" };
            _expectedSecondCoin = new() { Id = 2, Name = "Silver Piece", Initials = "SP" };

            _coinRepository = new Mock<ICoinRepository>();
            _coinRepository
                .Setup(m => m.GetAllCoins())
                .Returns(new List<Coin>(){ _expectedFirstCoin, _expectedSecondCoin });

            _coinRepository
                .Setup(m => m.AddCoin(It.IsAny<Coin>()))
                .Returns(_expectedFirstCoin);

            _coinRepository
                .Setup(m => m.GetCoinById(It.Is<long>(id => id == 1)))
                .Returns(_expectedFirstCoin);

            _coinRepository
                .Setup(m => m.GetCoinById(It.Is<long>(id => id != 1)))
                .Returns(value: null);

            _coinRepository
                .Setup(m => m.UpdateCoin(It.Is<Coin>(coin => coin.Id == 1)))
                .Returns(_expectedFirstCoin);

            _coinRepository
                .Setup(m => m.UpdateCoin(It.Is<Coin>(coin => coin.Id != 1)))
                .Returns(value: null);

            _coinRepository
                .Setup(m => m.DeleteCoin(It.Is<long>(id => id == 1)))
                .Returns(true);

            _coinRepository
                .Setup(m => m.DeleteCoin(It.Is<long>(id => id != 1)))
                .Returns(false);
        }

        [SetUp]
        public void SetUp()
        {
            _converter = new Mock<IConverter<Coin, CoinDTO>>();
            _converter
                .SetupSequence(m => m.Convert(It.IsAny<Coin>()))
                .Returns(new CoinDTO()
                {
                    Id = _expectedFirstCoin.Id,
                    Name = _expectedFirstCoin.Name,
                    Initials = _expectedFirstCoin.Initials
                })
                .Returns(new CoinDTO()
                {
                    Id = _expectedSecondCoin.Id,
                    Name = _expectedSecondCoin.Name,
                    Initials = _expectedSecondCoin.Initials
                });

            _service = new CoinService(_coinRepository.Object, _converter.Object);
        }

        [Test]
        public void GetCoins_ReturnsCoinDTOList()
        {
            // Arrange
            // Act
            List<CoinDTO> actual = _service.GetAllCoins().ToList();

            // Assert
            _coinRepository.Verify(repo => repo.GetAllCoins(), Times.Once);
            _converter.Verify(converter => converter.Convert(It.IsAny<Coin>()), Times.Exactly(2));

            Assert.That(actual, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(actual[0].Name, Is.EqualTo(_expectedFirstCoin.Name));
                Assert.That(actual[0].Initials, Is.EqualTo(_expectedFirstCoin.Initials));

                Assert.That(actual[1].Name, Is.EqualTo(_expectedSecondCoin.Name));
                Assert.That(actual[1].Initials, Is.EqualTo(_expectedSecondCoin.Initials));
            });
        }

        [Test]
        public void AddCoin_ValidRequest_ReturnsCoinDTO()
        {
            // Arrange
            CoinRequestDTO request = new()
            {
                Name = _expectedFirstCoin.Name,
                Initials = _expectedFirstCoin.Initials,
            };

            // Act
            CoinDTO actual = _service.AddCoin(request);

            // Assert
            _coinRepository.Verify(repo => repo.AddCoin(It.Is<Coin>(coin => 
                coin.Name == request.Name 
                && coin.Initials == request.Initials
            )), Times.Once);

            _converter.Verify(converter => converter.Convert(It.IsAny<Coin>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(_expectedFirstCoin.Id));
                Assert.That(actual.Name, Is.EqualTo(_expectedFirstCoin.Name));
                Assert.That(actual.Initials, Is.EqualTo(_expectedFirstCoin.Initials));
            });
        }

        [Test]
        public void GetCoinById_ValidId_ReturnsCoinDTO()
        {
            // Arrange
            long id = 1;
            var expectedCoinDTO = new CoinDTO 
            {
                Id = _expectedFirstCoin.Id,
                Name = _expectedFirstCoin.Name,
                Initials = _expectedFirstCoin.Initials 
            };

            // Act
            var actualCoinDTO = _service.GetCoinById(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualCoinDTO.Id, Is.EqualTo(expectedCoinDTO.Id));
                Assert.That(actualCoinDTO.Name, Is.EqualTo(expectedCoinDTO.Name));
                Assert.That(actualCoinDTO.Initials, Is.EqualTo(expectedCoinDTO.Initials));
            });

        }

        [Test]
        public void GetCoinById_InvalidId_ReturnsException()
        {
            // Arrange
            string actual = "";
            string expected = "There is no coin with the given ID";

            // Act
            try
            {
                _service.GetCoinById(3);
            }
            catch (KeyNotFoundException ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void UpdateCoin_ValidRequest_ReturnsCoinDTO()
        {
            // Arrange
            long id = 1;
            CoinRequestDTO request = new()
            {
                Name = _expectedFirstCoin.Name,
                Initials = _expectedFirstCoin.Initials,
            };

            // Act
            CoinDTO actual = _service.UpdateCoin(id, request);

            // Assert
            _coinRepository.Verify(repo => repo.UpdateCoin(It.Is<Coin>(
                coin => coin.Id == id 
                && coin.Name == request.Name 
                && coin.Initials == request.Initials
            )), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(id));
                Assert.That(actual.Name, Is.EqualTo(_expectedFirstCoin.Name));
                Assert.That(actual.Initials, Is.EqualTo(_expectedFirstCoin.Initials));
            });
        }

        [Test]
        public void UpdateCoin_InvalidId_ReturnsException()
        {
            // Arrange
            CoinRequestDTO request = new()
            {
                Name = _expectedFirstCoin.Name,
                Initials = _expectedFirstCoin.Initials
            };
            long id = 4;
            string actual = "";
            string expected = "There is no coin with the given ID";

            // Act
            try
            {
                _service.UpdateCoin(id, request);
            }
            catch (KeyNotFoundException ex)
            {
                actual = ex.Message;
            }

            // Assert
            _coinRepository.Verify(repo => repo.UpdateCoin(It.Is<Coin>(coin => 
                coin.Id == id 
                && coin.Name == request.Name 
                && coin.Initials == request.Initials
            )), Times.Once);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void DeleteCoin_InvalidId_ReturnsException()
        {
            // Arrange
            // Act
            bool actual = _service.DeleteCoin(3);

            // Assert
            _coinRepository.Verify(repo => repo.DeleteCoin(It.Is<long>(id => id == 3)), Times.Once);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void DeleteCoin_ValidId_ReturnsException()
        {
            // Arrange
            // Act
            bool actual = _service.DeleteCoin(1);

            // Assert
            _coinRepository.Verify(repo => repo.DeleteCoin(It.Is<long>(id => id == 1)), Times.Once);
            Assert.That(actual, Is.True);
        }
    }
}
