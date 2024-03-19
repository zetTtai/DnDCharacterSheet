using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;
public class SheetSkillConfiguration : IEntityTypeConfiguration<SheetSkill>
{
    public void Configure(EntityTypeBuilder<SheetSkill> builder)
    {
        _ = builder
            .HasKey(ss => new { ss.SheetId, ss.CapabilityId });

        _ = builder
            .HasOne(ss => ss.Sheet)
            .WithMany(s => s.SheetSkills)
            .HasForeignKey(sa => sa.SheetId);

        _ = builder
            .HasOne(ss => ss.Capability)
            .WithMany(s => s.SheetSkills)
            .HasForeignKey(ss => ss.CapabilityId);
    }
}
