using DnDCharacterSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDCharacterSheet.Infrastructure.Data.Configurations;
public class SheetSkillConfiguration : IEntityTypeConfiguration<SheetSkill>
{
    public void Configure(EntityTypeBuilder<SheetSkill> builder)
    {
        builder
            .HasOne(ss => new { ss.SheetId, ss.CapabilityId });

        builder
            .HasOne(ss => ss.Sheet)
            .WithMany(s => s.SheetSkills)
            .HasForeignKey(ss => ss.SheetId);

        builder
            .HasOne(ss => ss.Capability)
            .WithMany(s => s.SheetSkills)
            .HasForeignKey(ss => ss.CapabilityId);
    }
}
