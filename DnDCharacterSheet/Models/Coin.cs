namespace Models
{
    public class Coin
    {
        public long Id { get; set; }
        public required string Name { get; set; }
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
