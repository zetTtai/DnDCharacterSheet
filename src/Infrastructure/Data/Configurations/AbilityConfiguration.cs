using DnDCharacterSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDCharacterSheet.Infrastructure.Data.Configurations;
public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
{
    public void Configure(EntityTypeBuilder<Ability> builder)
    {
        builder
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
