using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;
public class SheetAbilityConfiguration : IEntityTypeConfiguration<SheetAbility>
{
    public void Configure(EntityTypeBuilder<SheetAbility> builder)
    {
        _ = builder
            .HasKey(sa => new { sa.SheetId, sa.AbilityId });

        _ = builder
            .HasOne(sa => sa.Sheet)
            .WithMany(s => s.SheetAbilities)
            .HasForeignKey(sa => sa.SheetId);

        _ = builder
            .HasOne(sa => sa.Ability)
            .WithMany(a => a.SheetAbilities)
            .HasForeignKey(sa => sa.AbilityId);
    }
}
