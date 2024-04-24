using DnDCharacterSheet.Domain.Entities;

namespace DnDCharacterSheet.Application.Sheets.Queries.GetSheetById;
public class AbilityDto
{
    public int Id { get; set; }
    public int Value { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SheetAbility, AbilityDto>()
                .ForMember(
                    d => d.Id,
                    opt => opt.MapFrom(sa => sa.AbilityId)
                );
        }
    }
}
