using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;
public class SheetCapabilityConfiguration : IEntityTypeConfiguration<SheetSkill>
{
    public void Configure(EntityTypeBuilder<SheetSkill> builder)
    {
        builder
            .HasKey(sc => new { sc.SheetId, sc.CapabilityId });

        builder
            .HasOne(sc => sc.Sheet)
            .WithMany(s => s.SheetSkills)
            .HasForeignKey(sc => sc.SheetId);

        builder
            .HasOne(sc => sc.Capability)
            .WithMany(c => c.SheetSkills)
            .HasForeignKey(sc => sc.CapabilityId);
    }
}
