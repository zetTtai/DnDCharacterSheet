using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CoinRequestDTO
    {
        [Required(ErrorMessage = "user (id) field is required.")]
        public required long User {  get; set; }
        [Required(ErrorMessage = "name field is required.")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "initials field is required.")]
        public required string Initials { get; set; }
        [Required(ErrorMessage = "timestamp is required.")]
        public required DateTime Timestamp { get; set; }
    }
}
