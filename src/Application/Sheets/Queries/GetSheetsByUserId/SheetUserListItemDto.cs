using DnDCharacterSheet.Application.Common.Interfaces;
using DnDCharacterSheet.Domain.Constants;
using DnDCharacterSheet.Domain.Entities;

namespace DnDCharacterSheet.Application;

public class SheetUserListItemDto
{
    public int Id { get; set; }
    public string? CharacterName { get; set; }
    public bool IsModifiedByAdmin { get; set; } = false;
    public DateTimeOffset LastModified { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Sheet, SheetUserListItemDto>();
        }
    }
}
