using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CurrencyRequestDTO
    {
        [Required(ErrorMessage = "user (id) field is required.")]
        public long User {  get; set; }
        [Required(ErrorMessage = "name field is required.")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "initials field is required.")]
        [MaxLength(2, ErrorMessage = "The maximum length for initials is 2 characters.")]
        public required string Initials { get; set; }
        [Required(ErrorMessage = "timestamp is required.")]
        public DateTime Timestamp { get; set; }
    }
}
