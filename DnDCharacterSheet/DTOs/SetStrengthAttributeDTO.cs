using Enums;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class SetStrengthAttributeDTO
    {
        [Required(ErrorMessage = "Value is required.")]
        public int Value { get; set; }
        [Required(ErrorMessage = "Method is required.")]
        public MethodsToIncreaseAttributes Method { get; set; }
    }
}
