using DnDCharacterSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDCharacterSheet.Infrastructure.Data.Configurations;
public class SheetAbilityConfiguration : IEntityTypeConfiguration<SheetAbility>
{
    public void Configure(EntityTypeBuilder<SheetAbility> builder)
    {
        builder
            .HasKey(sa => new { sa.SheetId, sa.AbilityId });

        builder
            .HasOne(sa => sa.Sheet)
            .WithMany(s => s.SheetAbilities)
            .HasForeignKey(sa => sa.SheetId);

        builder
            .HasOne(sa => sa.Ability)
            .WithMany(a => a.SheetAbilities)
            .HasForeignKey(sa => sa.AbilityId);
    }
}
