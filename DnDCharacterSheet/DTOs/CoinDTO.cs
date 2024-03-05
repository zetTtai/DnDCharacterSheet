using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CoinDTO
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Initials { get; set; }
    }
}
