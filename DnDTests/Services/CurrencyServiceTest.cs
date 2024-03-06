using DTOs;
using Interfaces;
using Models;
using Moq;
using Services;

namespace DnDTests.Services
{
    internal class CurrencyServiceTest
    {
        private CurrencyService _service;
        private Mock<ICurrencyRepository> _currencyRepository;
        private Mock<IConverter<Currency, CurrencyDTO>> _converter;
        private Currency _expectedFirstCurrency;
        private Currency _expectedSecondCurrency;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _expectedFirstCurrency = new() { Id = 1, Name = "Gold Piece", Initials = "GP" };
            _expectedSecondCurrency = new() { Id = 2, Name = "Silver Piece", Initials = "SP" };

            _currencyRepository = new Mock<ICurrencyRepository>();
            _currencyRepository
                .Setup(m => m.GetAllCurrency())
                .Returns(new List<Currency>(){ _expectedFirstCurrency, _expectedSecondCurrency });

            _currencyRepository
                .Setup(m => m.AddCurrency(It.IsAny<Currency>()))
                .Returns(_expectedFirstCurrency);

            _currencyRepository
                .Setup(m => m.GetCurrencyById(It.Is<long>(id => id == 1)))
                .Returns(_expectedFirstCurrency);

            _currencyRepository
                .Setup(m => m.GetCurrencyById(It.Is<long>(id => id != 1)))
                .Returns(value: null);

            _currencyRepository
                .Setup(m => m.UpdateCurrency(It.Is<Currency>(coin => coin.Id == 1)))
                .Returns(_expectedFirstCurrency);

            _currencyRepository
                .Setup(m => m.UpdateCurrency(It.Is<Currency>(coin => coin.Id != 1)))
                .Returns(value: null);

            _currencyRepository
                .Setup(m => m.DeleteCurrency(It.Is<long>(id => id == 1)))
                .Returns(true);

            _currencyRepository
                .Setup(m => m.DeleteCurrency(It.Is<long>(id => id != 1)))
                .Returns(false);
        }

        [SetUp]
        public void SetUp()
        {
            _converter = new Mock<IConverter<Currency, CurrencyDTO>>();
            _converter
                .SetupSequence(m => m.Convert(It.IsAny<Currency>()))
                .Returns(new CurrencyDTO()
                {
                    Id = _expectedFirstCurrency.Id,
                    Name = _expectedFirstCurrency.Name,
                    Initials = _expectedFirstCurrency.Initials
                })
                .Returns(new CurrencyDTO()
                {
                    Id = _expectedSecondCurrency.Id,
                    Name = _expectedSecondCurrency.Name,
                    Initials = _expectedSecondCurrency.Initials
                });

            _service = new CurrencyService(_currencyRepository.Object, _converter.Object);
        }

        [Test]
        public void GetCurrencies_ReturnsCurrencyDTOList()
        {
            // Arrange
            // Act
            List<CurrencyDTO> actual = _service.GetAllCurrencies().ToList();

            // Assert
            _currencyRepository.Verify(repo => repo.GetAllCurrency(), Times.Once);
            _converter.Verify(converter => converter.Convert(It.IsAny<Currency>()), Times.Exactly(2));

            Assert.That(actual, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(actual[0].Name, Is.EqualTo(_expectedFirstCurrency.Name));
                Assert.That(actual[0].Initials, Is.EqualTo(_expectedFirstCurrency.Initials));

                Assert.That(actual[1].Name, Is.EqualTo(_expectedSecondCurrency.Name));
                Assert.That(actual[1].Initials, Is.EqualTo(_expectedSecondCurrency.Initials));
            });
        }

        [Test]
        public void AddCurrency_ValidRequest_ReturnsCurrencyDTO()
        {
            // Arrange
            CurrencyRequestDTO request = new()
            {
                Name = _expectedFirstCurrency.Name,
                Initials = _expectedFirstCurrency.Initials
            };

            // Act
            CurrencyDTO actual = _service.AddCurrency(request);

            // Assert
            _currencyRepository.Verify(repo => repo.AddCurrency(It.Is<Currency>(coin => 
                coin.Name == request.Name 
                && coin.Initials == request.Initials
            )), Times.Once);

            _converter.Verify(converter => converter.Convert(It.IsAny<Currency>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(_expectedFirstCurrency.Id));
                Assert.That(actual.Name, Is.EqualTo(_expectedFirstCurrency.Name));
                Assert.That(actual.Initials, Is.EqualTo(_expectedFirstCurrency.Initials));
            });
        }

        [Test]
        public void GetCurrencyById_ValidId_ReturnsCurrencyDTO()
        {
            // Arrange
            long id = 1;
            var expectedCurrencyDTO = new CurrencyDTO 
            {
                Id = _expectedFirstCurrency.Id,
                Name = _expectedFirstCurrency.Name,
                Initials = _expectedFirstCurrency.Initials 
            };

            // Act
            var actualCurrencyDTO = _service.GetCurrencyById(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualCurrencyDTO.Id, Is.EqualTo(expectedCurrencyDTO.Id));
                Assert.That(actualCurrencyDTO.Name, Is.EqualTo(expectedCurrencyDTO.Name));
                Assert.That(actualCurrencyDTO.Initials, Is.EqualTo(expectedCurrencyDTO.Initials));
            });

        }

        [Test]
        public void GetCurrencyById_InvalidId_ReturnsException()
        {
            // Arrange
            string actual = "";
            string expected = "There is no currency with the given ID";

            // Act
            try
            {
                _service.GetCurrencyById(3);
            }
            catch (KeyNotFoundException ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void UpdateCurrency_ValidRequest_ReturnsCurrencyDTO()
        {
            // Arrange
            long id = 1;
            CurrencyRequestDTO request = new()
            {
                Name = _expectedFirstCurrency.Name,
                Initials = _expectedFirstCurrency.Initials,
            };

            // Act
            CurrencyDTO actual = _service.UpdateCurrency(id, request);

            // Assert
            _currencyRepository.Verify(repo => repo.UpdateCurrency(It.Is<Currency>(
                coin => coin.Id == id 
                && coin.Name == request.Name 
                && coin.Initials == request.Initials
            )), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(id));
                Assert.That(actual.Name, Is.EqualTo(_expectedFirstCurrency.Name));
                Assert.That(actual.Initials, Is.EqualTo(_expectedFirstCurrency.Initials));
            });
        }

        [Test]
        public void UpdateCurrency_InvalidId_ReturnsException()
        {
            // Arrange
            CurrencyRequestDTO request = new()
            {
                Name = _expectedFirstCurrency.Name,
                Initials = _expectedFirstCurrency.Initials
            };
            long id = 4;
            string actual = "";
            string expected = "There is no currency with the given ID";

            // Act
            try
            {
                _service.UpdateCurrency(id, request);
            }
            catch (KeyNotFoundException ex)
            {
                actual = ex.Message;
            }

            // Assert
            _currencyRepository.Verify(repo => repo.UpdateCurrency(It.Is<Currency>(coin => 
                coin.Id == id 
                && coin.Name == request.Name 
                && coin.Initials == request.Initials
            )), Times.Once);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void DeleteCurrency_InvalidId_ReturnsException()
        {
            // Arrange
            // Act
            bool actual = _service.DeleteCurrency(3);

            // Assert
            _currencyRepository.Verify(repo => repo.DeleteCurrency(It.Is<long>(id => id == 3)), Times.Once);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void DeleteCurrency_ValidId_ReturnsException()
        {
            // Arrange
            // Act
            bool actual = _service.DeleteCurrency(1);

            // Assert
            _currencyRepository.Verify(repo => repo.DeleteCurrency(It.Is<long>(id => id == 1)), Times.Once);
            Assert.That(actual, Is.True);
        }
    }
}
