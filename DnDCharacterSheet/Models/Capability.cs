
using Enums;

namespace Models
{
    public class Capability
    {
        public required string Name { get; set; }
        public required CharacterAttributes AsociatedAttribute { get; set; }
        public required string Value { get; set; }
    }
}
