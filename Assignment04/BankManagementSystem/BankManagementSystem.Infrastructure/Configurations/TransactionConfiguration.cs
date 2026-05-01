using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.Configurations;

public sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .HasIndex(t => t.Number)
            .IsUnique();
        builder
            .Property(t => t.Number)
            .HasColumnOrder(2)
            .IsRequired();
        builder
            .Property(t => t.Date)
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnOrder(3)
            .IsRequired();
        builder
            .Property(t => t.Amount)
            .HasColumnType("DECIMAL(15,2)")
            .HasColumnOrder(4)
            .IsRequired();
        builder
            .Property(t => t.Type)
            .HasConversion<string>()
            .HasMaxLength(10)
            .HasColumnOrder(5)
            .IsRequired();
        builder
            .Property(t => t.Note)
            .HasColumnOrder(6)
            .IsRequired();
        builder
            .Property(t => t.AccountId)
            .HasColumnOrder(7)
            .IsRequired();
    }
}