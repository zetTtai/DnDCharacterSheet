using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;
internal class SheetConfiguration : IEntityTypeConfiguration<Sheet>
{
    public void Configure(EntityTypeBuilder<Sheet> builder)
    {
        _ = builder
            .Property(t => t.CharacterName)
            .HasMaxLength(200)
            .IsRequired();

        _ = builder
            .HasMany(s => s.SheetAbilities)
            .WithOne(sa => sa.Sheet)
            .HasForeignKey(sa => sa.SheetId);

        _ = builder
            .HasMany(s => s.SheetSkills)
            .WithOne(sc => sc.Sheet)
            .HasForeignKey(sc => sc.SheetId);

        _ = builder
            .HasMany(s => s.SheetSavingThrows)
            .WithOne(sc => sc.Sheet)
            .HasForeignKey(sc => sc.SheetId);
    }
}
