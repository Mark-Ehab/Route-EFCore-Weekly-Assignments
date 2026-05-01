using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.Configurations;

public sealed class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder
            .HasIndex(b => b.Code)
            .IsUnique();
        builder
            .Property(b => b.Code)
            .HasMaxLength(7)
            .HasColumnOrder(2)
            .IsRequired();
        builder
            .Property(b => b.Name)
            .HasMaxLength(40)
            .HasColumnOrder(3)
            .IsRequired();
        builder
            .Property(b => b.Address)
            .HasMaxLength(100)
            .HasColumnOrder(4)
            .IsRequired();
        builder
            .HasIndex(b => b.PhoneNumber)
            .IsUnique();
        builder
            .Property(b => b.PhoneNumber)
            .HasMaxLength(20)
            .HasColumnOrder(5)
            .IsRequired();
        builder
            .Property(b => b.ManagerId)
            .HasColumnOrder(6)
            .IsRequired();
        builder
            .HasOne(b => b.Manager)
            .WithOne(m => m.Branch)
            .HasForeignKey<Branch>(b => b.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}