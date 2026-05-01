using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.Configurations;

public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .HasIndex(a => a.Number)
            .IsUnique();
        builder
            .Property(a => a.Number)
            .HasMaxLength(8)
            .HasColumnOrder(2)
            .IsRequired();
        builder
            .Property(a => a.Balance)
            .HasColumnType("DECIMAL(15,2)")
            .HasColumnOrder(3)
            .IsRequired();
        builder
            .Property(a => a.Type)
            .HasConversion<string>()
            .HasMaxLength(8)
            .HasColumnOrder(4)
            .IsRequired();
        builder
            .Property(a => a.OpeningDate)
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnOrder(6)
            .IsRequired();
        builder
            .Property(a => a.BranchId)
            .HasColumnOrder(7)
            .IsRequired();
        builder
            .HasOne(a => a.Branch)
            .WithMany(b => b.Accounts)
            .HasForeignKey(a => a.BranchId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(a => a.AccountCustomers)
            .WithOne(ac => ac.Account)
            .HasForeignKey(ac => ac.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}