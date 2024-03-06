namespace Models
{
    public class Currency
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Initials { get; set; }

        public Currency()
        {
            Id = 0;
            Name = "gold piece";
            Initials = "gp";
        }

        public Currency(int id)
        {
            Id = id;
            Name = "gold piece";
            Initials = "gp";
        }
    }
}
