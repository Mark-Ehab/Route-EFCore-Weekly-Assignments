using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .Property(c => c.FullName)
            .HasMaxLength(40)
            .HasColumnOrder(2)
            .IsRequired();
        builder
            .HasIndex(c => c.Email)
            .IsUnique();
        builder
            .Property(c => c.Email)
            .HasMaxLength(80)
            .HasColumnOrder(3)
            .IsRequired();
        builder
            .HasIndex(c => c.NationalId)
            .IsUnique();
        builder
            .Property(c => c.NationalId)
            .HasMaxLength(20)
            .HasColumnOrder(4)
            .IsRequired();
        builder
            .HasIndex(c => c.PhoneNumber)
            .IsUnique();
        builder
            .Property(c => c.PhoneNumber)
            .HasMaxLength(20)
            .HasColumnOrder(5)
            .IsRequired();
        builder
            .Property(c => c.Address)
            .HasMaxLength(100)
            .HasColumnOrder(6)
            .IsRequired();
        builder
            .Property(c => c.Type)
            .HasConversion<string>()
            .HasMaxLength(10)
            .HasColumnOrder(7)
            .IsRequired();
        builder
            .Property(c => c.DateOfBirth)
            .HasColumnOrder(8)
            .IsRequired();
        builder
            .HasMany(c => c.CustomerAccounts)
            .WithOne(ac => ac.Customer)
            .HasForeignKey(ac => ac.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}