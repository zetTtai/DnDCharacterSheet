using DnDCharacterSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DnDCharacterSheet.Domain.Constants;
using DnDCharacterSheet.Domain.ValueObjects;

namespace DnDCharacterSheet.Infrastructure.Data.Configurations;
public class SheetConfiguration : IEntityTypeConfiguration<Sheet>
{
    public void Configure(EntityTypeBuilder<Sheet> builder)
    {
        builder
            .Property(s => s.CharacterName)
            .IsRequired()
            .HasMaxLength(SheetConstants.CharacterNameMaxLength);

        builder
            .HasMany(s => s.SheetAbilities)
            .WithOne(sa => sa.Sheet)
            .HasForeignKey(sa => sa.SheetId);

        builder
            .HasMany(s => s.SheetSkills)
            .WithOne(ss => ss.Sheet)
            .HasForeignKey(ss => ss.SheetId);

        builder
            .HasMany(s => s.SheetSavingThrows)
            .WithOne(sst => sst.Sheet)
            .HasForeignKey(sst => sst.SheetId);

        builder.
            OwnsOne(s => s.Money, owned =>
            {
                var currencies = typeof(Money).GetProperties();
                foreach (var currency in currencies)
                {
                    owned.Property(currency.Name).HasColumnName(currency.Name);
                }
            });
    }
}
