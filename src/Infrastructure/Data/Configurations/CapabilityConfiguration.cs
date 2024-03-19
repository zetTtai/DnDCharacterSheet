﻿using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Configurations;
public class CapabilityConfiguration : IEntityTypeConfiguration<Capability>
{
    public void Configure(EntityTypeBuilder<Capability> builder)
    {
        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasOne(c => c.Ability)
            .WithMany(a => a.Capabilities)
            .HasForeignKey(c => c.AbilityId);
    }
}