using Converters;
using DTOs;
using Interfaces;
using Models;

namespace DnDTests.Converters
{
    internal class CurrencyConverterTest
    {
        private IConverter<Currency, CurrencyDTO> _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = new CurrencyConverter();
        }
        [Test]
        public void Convert_Currency_ReturnCurrencyDTO()
        {
            // Arrange
            Currency currency = new()
            {
                Id = 1,
                Name = "Test",
                Initials = "TT",
            };

            CurrencyDTO expected = new()
            {
                Id = 1,
                Name = "Test",
                Initials = "TT",
            };

            // Act
            var actual = _mapper.Convert(currency);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(expected.Id));
                Assert.That(actual.Name, Is.EqualTo(expected.Name));
                Assert.That(actual.Initials, Is.EqualTo(expected.Initials));
            });
        }

        [Test]
        public void Convert_List_ReturnCurrencyDTOList()
        {
            // Arrange
            IEnumerable<Currency> currencies = [
                new Currency()
                {
                    Id = 1,
                    Name = "Test",
                    Initials = "TT",
                },
                new Currency()
                {
                    Id = 2,
                    Name = "Test2",
                    Initials = "TG",
                },
            ];
            List<CurrencyDTO> expected = [
                new CurrencyDTO()
                {
                    Id = 1,
                    Name = "Test",
                    Initials = "TT",
                },
                new CurrencyDTO()
                {
                    Id = 2,
                    Name = "Test2",
                    Initials = "TG",
                }
            ];

            // Act
            List<CurrencyDTO> actual = _mapper.Convert(currencies).ToList();

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
                    Assert.That(actual[i].Name, Is.EqualTo(expected[i].Name));
                    Assert.That(actual[i].Initials, Is.EqualTo(expected[i].Initials));
                });
            }
        }
    }
}
