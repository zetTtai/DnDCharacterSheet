
using Enums;
using System.Collections;

namespace Models
{
    public class Capability
    {
        public required string Name { get; set; }
        public required CharacterAttributes AssociatedAttribute { get; set; }
        public required string Value { get; set; }
    }
}
