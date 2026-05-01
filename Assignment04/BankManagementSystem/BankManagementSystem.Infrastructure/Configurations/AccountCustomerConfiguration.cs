using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.Configurations;

public sealed class AccountCustomerConfiguration : IEntityTypeConfiguration<AccountCustomer>
{
    public void Configure(EntityTypeBuilder<AccountCustomer> builder)
    {
        builder
            .Property(ac => ac.AccountId)
            .HasColumnOrder(2);
        builder
            .Property(ac => ac.CustomerId)
            .HasColumnOrder(3);
        builder
            .Property(ac => ac.OwnershipStartDate)
            .HasColumnOrder(4)
            .IsRequired();
        builder
            .Property(ac => ac.OwnershipType)
            .HasConversion<string>()
            .HasMaxLength(13)
            .HasColumnOrder(5)
            .IsRequired();
        builder
            .Property(ac => ac.AccountStatus)
            .HasConversion<string>()
            .HasMaxLength(6)
            .HasColumnOrder(6)
            .IsRequired();
    }
}