﻿using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;
public class SheetSavingThrowConfiguration : IEntityTypeConfiguration<SheetSavingThrow>
{
    public void Configure(EntityTypeBuilder<SheetSavingThrow> builder)
    {
        _ = builder
            .HasKey(sst => new { sst.SheetId, sst.CapabilityId });

        _ = builder
            .HasOne(sst => sst.Sheet)
            .WithMany(s => s.SheetSavingThrows)
            .HasForeignKey(sst => sst.SheetId);

        _ = builder
            .HasOne(sst => sst.Capability)
            .WithMany(c => c.SheetSavingThrows)
            .HasForeignKey(sst => sst.CapabilityId);
    }
}
