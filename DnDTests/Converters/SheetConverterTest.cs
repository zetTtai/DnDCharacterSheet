using DTOs;
using Interfaces;
using Converters;
using Models;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace DnDTests.Converters
{
    internal class SheetConverterTest
    {
        private IConverter<Sheet, SheetDTO> _converter;
        private IConverter<Capability, CapabilityDTO> _capabilityConverter;
        private Mock<IConverter<Capability, CapabilityDTO>> _capabilityConverterMock;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _capabilityConverterMock = new Mock<IConverter<Capability, CapabilityDTO>>();
            _capabilityConverterMock
                .Setup(m => m.Convert(It.IsAny<IEnumerable<Capability>>()))
                .Returns([]);
            _capabilityConverter = _capabilityConverterMock.Object;
            _converter = new SheetConverter(_capabilityConverter);

        }

        [Test]
        public void Convert_Sheet_ReturnsSheetDTO()
        {
            // Arrange
            Sheet sheet = new(1);
            SheetDTO expected = new()
            {
                Id = 1,
                StrengthScore = "",
            };

            // Act
            SheetDTO actual = _converter.Convert(sheet);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.Id, Is.EqualTo(expected.Id));
                Assert.That(actual.StrengthScore, Is.EqualTo(expected.StrengthScore));
            });
        }

        [Test]
        public void Convert_List_ReturnsSheetDTOList()
        {
            // Arrange
            IEnumerable<Sheet> sheets = [
                new(1),
                new(2),
                new(3),
            ];

            List<SheetDTO> expected = [
                new() { Id = 1, StrengthScore = "" },
                new() { Id = 2, StrengthScore = "" },
                new() { Id = 3, StrengthScore = "" },
            ];

            // Act
            List<SheetDTO> actual = (List<SheetDTO>)_converter.Convert(sheets);

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
                    Assert.That(actual[i].StrengthScore, Is.EqualTo(expected[i].StrengthScore));
                });
            }
        }
    }
}
