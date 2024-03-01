using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Coin
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public required string Name { get; set; }
        [Column("initials")]
        public required string Initials { get; set; }

        public Coin()
        {
            Id = 0;
            Name = "gold piece";
            Initials = "gp";
        }

        public Coin(int id)
        {
            Id = id;
            Name = "gold piece";
            Initials = "gp";
        }
    }
}
