using Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs;

//public record SetAbilityRequestDTO(
//    [property: Required] int Value,
//    [property: Required] MethodsToIncreaseAbilities Method
//);
public class SetAbilityRequestDTO
{
    [Required]
    public int Value { get; set; }

    [Required]
    public MethodsToIncreaseAbilities Method {  get; set; }
}

