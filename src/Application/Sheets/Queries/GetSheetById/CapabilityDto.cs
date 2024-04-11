using DnDCharacterSheet.Application.Sheets.Queries.GetSheetById;
using DnDCharacterSheet.Domain.Entities;

namespace DnDCharacterSheet.Application;

public class CapabilityDto
{
    public int Id { get; set; }

    public bool Proficiency { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SheetCapability, CapabilityDto>()
                .ForMember(
                    d => d.Id,
                    opt => opt.MapFrom(sc => sc.CapabilityId)
                );
        }
    }
}
