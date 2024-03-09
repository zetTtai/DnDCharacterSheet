using Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record SetStrengthAttributeDTO(
    [property: Required] int Value,
    [property: Required] MethodsToIncreaseAbilities Method
);
