using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using System.Text;

namespace BankManagementSystem.Application.Configurations;

public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        /* Apply TPC Inheritance Mapping Strategy */
        builder
            .HasKey(be => be.Id);
        builder
            .Property(be => be.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()")
            .HasColumnOrder(1)
            .IsRequired();
        builder
            .Property(be => be.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();
        builder
            .Property(be => be.CreatedAt)
            .IsRequired();
        builder
            .Property(be => be.ModifiedBy)
            .HasMaxLength(100);
        builder
            .Property(be => be.DeletedBy)
            .HasMaxLength(100);
        builder
            .Property(be => be.IsDeleted)
            .IsRequired();
        builder
            .HasQueryFilter(be => be.IsDeleted == false);
        builder
            .UseTpcMappingStrategy();
    }
}