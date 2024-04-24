using DnDCharacterSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDCharacterSheet.Infrastructure.Data.Configurations;
public class SheetSavingThrowConfiguration : IEntityTypeConfiguration<SheetSavingThrow>
{
    public void Configure(EntityTypeBuilder<SheetSavingThrow> builder)
    {
        builder
            .HasKey(sst => new { sst.SheetId, sst.CapabilityId });

        builder
            .HasOne(sst => sst.Sheet)
            .WithMany(s => s.SheetSavingThrows)
            .HasForeignKey(sst => sst.SheetId);

        builder
            .HasOne(sst => sst.Capability)
            .WithMany(c => c.SheetSavingThrows)
            .HasForeignKey(sst => sst.CapabilityId);
    }
}
