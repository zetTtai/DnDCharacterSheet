﻿using DTOs;
using Enums;
using Interfaces;
using Models;
using Moq;
using Services;

namespace DnDTests
{
    internal class SheetServiceTest
    {
        private Mock<IModifierCalculatorService> _modifierCalculatorMock;
        private readonly string _expectedModifier = "+4";

        [SetUp]
        public void Setup()
        {
            _modifierCalculatorMock = new Mock<IModifierCalculatorService>();
            _modifierCalculatorMock.Setup(m => m.ValueToModifier(It.IsAny<int>())).Returns(_expectedModifier);
            SheetService service = new(_modifierCalculatorMock.Object);
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ModifyStrengthScore_ReturnsSheet()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            Sheet expected = new()
            {
                StrengthScore = _expectedModifier
            };

            // Act
            Sheet actual = service.SetStrengthAttribute(new Sheet(), 3);

            // Assert
            Assert.That(actual.StrengthScore, Is.EqualTo(expected.StrengthScore));
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ModifySkills_ReturnsSheet()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            Sheet expected = new()
            {
                StrengthScore = _expectedModifier
            };
            expected.Skills[0].Value = _expectedModifier;

            // Act
            Sheet actual = service.SetStrengthAttribute(new Sheet(), 3);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.StrengthScore, Is.EqualTo(expected.StrengthScore));
                Assert.That(actual.Skills, Has.Count.EqualTo(expected.Skills.Count));
            });
            for (int i = 0; i < expected.Skills.Count; i++)
            {
                Assert.That(actual.Skills[i].Value, Is.EqualTo(expected.Skills[i].Value));
            }
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ModifySavingThrows_ReturnsSheet()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            Sheet expected = new()
            {
                StrengthScore = _expectedModifier
            };
            expected.SavingThrows[0].Value = _expectedModifier;

            // Act
            Sheet actual = service.SetStrengthAttribute(new Sheet(), 18);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.StrengthScore, Is.EqualTo(expected.StrengthScore));
                Assert.That(actual.SavingThrows, Has.Count.EqualTo(expected.SavingThrows.Count));
            });

            for (int i = 0; i < expected.SavingThrows.Count; i++)
            {
                Assert.That(actual.SavingThrows[i].Value, Is.EqualTo(expected.SavingThrows[i].Value));
            }
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ValueLowerThanThree_ReturnsException()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            string expected = "Invalid value, must be between 3 and 18";
            string actual = "";
            // Act
            try
            {
                Sheet sheet = service.SetStrengthAttribute(new Sheet(), 2);
            } catch (Exception ex)
            {
                actual = ex.Message;
            }
            
            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SetStrenghtScore_RollingDice_ValueHigherThan18_ReturnsException()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            string expected = "Invalid value, must be between 3 and 18";
            string actual = "";
            // Act
            try
            {
                Sheet sheet = service.SetStrengthAttribute(new Sheet(), 19);
            }
            catch (Exception ex)
            {
                actual = ex.Message;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToDTO_EmptyList_ReturnCapabilityDTOList()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            List<Capability> capabilities = [];

            // Act
            List<CapabilityDTO> actual = service.ConvertToDTO(capabilities);

            // Assert
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public void ConvertToDTO_ConvertSkills_ReturnCapabilityDTOList()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            List<Capability> capabilities = [
                new Capability()
                {
                    Name = "Athletics",
                    AsociatedAttribute = CharacterAttributes.STR,
                    Value = string.Empty,
                },
                new Capability()
                {
                    Name = "Acrobatics",
                    AsociatedAttribute = CharacterAttributes.DEX,
                    Value = string.Empty,
                },
            ];
            List<CapabilityDTO> expected = [
                new CapabilityDTO()
                {
                    Id = "Athletics",
                    AsociatedScore = "STR",
                    Value = string.Empty,
                },
                new CapabilityDTO()
                {
                    Id = "Acrobatics",
                    AsociatedScore = "DEX",
                    Value = string.Empty,
                }
            ];

            // Act
            List<CapabilityDTO> actual = service.ConvertToDTO(capabilities);

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
                    Assert.That(actual[i].AsociatedScore, Is.EqualTo(expected[i].AsociatedScore));
                });
            }
        }

        [Test]
        public void ConvertToDTO_ConvertSavingThrows_ReturnCapabilityDTOList()
        {
            // Arrange
            SheetService service = new(_modifierCalculatorMock.Object);
            List<Capability> capabilities = [
                new Capability()
                {
                    Name = "Strength",
                    AsociatedAttribute = CharacterAttributes.STR,
                    Value = string.Empty,
                },
                new Capability()
                {
                    Name = "Dexterity",
                    AsociatedAttribute = CharacterAttributes.DEX,
                    Value = string.Empty,
                },
            ];
            List<CapabilityDTO> expected = [
                new CapabilityDTO()
                {
                    Id = "Strength",
                    AsociatedScore = "STR",
                    Value = "",
                },
                new CapabilityDTO()
                {
                    Id = "Dexterity",
                    AsociatedScore = "DEX",
                    Value = "",
                }
            ];

            // Act
            List<CapabilityDTO> actual = service.ConvertToDTO(capabilities, false);

            // Assert
            Assert.That(actual, Has.Count.EqualTo(expected.Count));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
                    Assert.That(actual[i].AsociatedScore, Is.EqualTo(expected[i].AsociatedScore));
                });
            }
        }
    }
}
