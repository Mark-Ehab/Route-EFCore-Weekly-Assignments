using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.Configurations;

public sealed class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder
            .Property(m => m.Fullname)
            .HasMaxLength(40)
            .HasColumnOrder(2)
            .IsRequired();
        builder
            .HasIndex(m => m.Email)
            .IsUnique();
        builder
            .Property(m => m.Email)
            .HasMaxLength(80)
            .HasColumnOrder(3)
            .IsRequired();
        builder
            .HasIndex(m => m.PhoneNumber)
            .IsUnique();
        builder
            .Property(m => m.PhoneNumber)
            .HasMaxLength(20)
            .HasColumnOrder(4)
            .IsRequired();
        builder
            .Property(m => m.HireDate)
            .HasColumnOrder(5)
            .IsRequired();
    }
}