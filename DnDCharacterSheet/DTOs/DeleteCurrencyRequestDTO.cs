using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class DeleteCurrencyRequestDTO
    {
        [Required(ErrorMessage = "user (id) field is required.")]
        public required long User { get; set; }
        [Required(ErrorMessage = "timestamp is required.")]
        public required DateTime Timestamp { get; set; }
    }
}
