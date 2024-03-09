using Converters;
using DTOs;
using Interfaces;
using Models;
using Moq;

namespace DnDTests.Converters;

internal class SheetConverterTest
{
    private IConverter<Sheet, SheetDTO> _converter;
    private Mock<IConverter<Capability, CapabilityDTO>> _capabilityConverterMock;
    private Mock<IConverter<Models.Ability, AttributeDTO>> _attributeConverterMock;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _capabilityConverterMock = new Mock<IConverter<Capability, CapabilityDTO>>();
        _ = _capabilityConverterMock
            .Setup(m => m.Convert(It.IsAny<IEnumerable<Capability>>()))
            .Returns([]);

        _attributeConverterMock = new Mock<IConverter<Models.Ability, AttributeDTO>>();
        _ = _attributeConverterMock
            .Setup(m => m.Convert(It.IsAny<IEnumerable<Models.Ability>>()))
            .Returns([]);

        _converter = new SheetConverter(_capabilityConverterMock.Object, _attributeConverterMock.Object);

    }

    [Test]
    public void Convert_Sheet_ReturnsSheetDTO()
    {
        // Arrange
        Sheet sheet = new(1);
        SheetDTO expected = new()
        {
            Id = 1,
            Attributes =
            [
                new AttributeDTO { Modifier = "", Value = "", Name = "STR" },
                new AttributeDTO { Modifier = "", Value = "", Name = "DEX" },
                new AttributeDTO { Modifier = "", Value = "", Name = "CON" },
                new AttributeDTO { Modifier = "", Value = "", Name = "INT" },
                new AttributeDTO { Modifier = "", Value = "", Name = "WIS" },
                new AttributeDTO { Modifier = "", Value = "", Name = "CHA" },
            ],
        };

        // Act
        SheetDTO actual = _converter.Convert(sheet);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
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
            new() { Id = 1 },
            new() { Id = 2 },
            new() { Id = 3 },
        ];

        // Act
        List<SheetDTO> actual = _converter.Convert(sheets).ToList();

        // Assert
        Assert.That(actual, Has.Count.EqualTo(expected.Count));
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
            });
        }
    }
}
