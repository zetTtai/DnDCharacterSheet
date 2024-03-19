﻿using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;
internal class SheetConfiguration : IEntityTypeConfiguration<Sheet>
{
    public void Configure(EntityTypeBuilder<Sheet> builder)
    {
        builder
            .Property(t => t.CharacterName)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .HasMany(s => s.SheetAbilities)
            .WithOne(sa => sa.Sheet)
            .HasForeignKey(sa => sa.SheetId);

        builder
            .HasMany(s => s.SheetSkills)
            .WithOne(sc => sc.Sheet)
            .HasForeignKey(sc => sc.SheetId);

        builder
            .HasMany(s => s.SheetSavingThrows)
            .WithOne(sc => sc.Sheet)
            .HasForeignKey(sc => sc.SheetId);
    }
}
