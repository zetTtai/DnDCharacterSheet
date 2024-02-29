using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CoinRequestDTO
    {
        [Required(ErrorMessage = "user is required.")]
        public int User {  get; set; }
        public string? Name { get; set; }
        public string? Initials { get; set; }
        [Required(ErrorMessage = "timestamp is required.")]
        public DateTime Timestamp { get; set; }
    }
}
