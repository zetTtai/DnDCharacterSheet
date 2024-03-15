using Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record SetAbilityRequestDTO(
    [Required] int Value,
    [Required] MethodsToIncreaseAbilities Method
);

