using Converters;
using DTOs;
using Enums;
using Interfaces;
using Models;

namespace DnDTests.Converters
{
    internal class CoinConverterTest
    {
        private IConverter<Coin, CoinDTO> _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = new CoinConverter();
        }
        [Test]
        public void Convert_Coin_ReturnCoinDTO()
        {
            // Arrange
            Coin coin = new()
            {
                Id = 1,
                Name = "Test",
                Initials = "TT",
            };

            CoinDTO expected = new()
            {
                Id = 1,
                Name = "Test",
                Initials = "TT",
            };

            // Act
            var actual = _mapper.Convert(coin);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(expected.Id));
                Assert.That(actual.Name, Is.EqualTo(expected.Name));
                Assert.That(actual.Initials, Is.EqualTo(expected.Initials));
            });
        }

        [Test]
        public void Convert_List_ReturnCoinDTOList()
        {
            // Arrange
            IEnumerable<Coin> coins = [
                new Coin()
                {
                    Id = 1,
                    Name = "Test",
                    Initials = "TT",
                },
                new Coin()
                {
                    Id = 2,
                    Name = "Test2",
                    Initials = "TG",
                },
            ];
            List<CoinDTO> expected = [
                new CoinDTO()
                {
                    Id = 1,
                    Name = "Test",
                    Initials = "TT",
                },
                new CoinDTO()
                {
                    Id = 2,
                    Name = "Test2",
                    Initials = "TG",
                }
            ];

            // Act
            List<CoinDTO> actual = _mapper.Convert(coins).ToList();

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
