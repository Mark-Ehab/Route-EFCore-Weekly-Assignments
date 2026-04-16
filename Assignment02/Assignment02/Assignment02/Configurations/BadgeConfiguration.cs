using Assignment02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02.Configurations;

public class BadgeConfiguration : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder
              .HasKey(b => b.Id);
        builder
              .Property(b => b.Id)
              .UseIdentityColumn(1000,1);
        builder
              .Property(b => b.IssueDate)
              .HasDefaultValueSql("SYSDATETIME()");
        builder
              .Property(b => b.Tier)
              .HasMaxLength(8);
        builder
            .ToTable(t => t.HasCheckConstraint("CK_Badges_Tier", "Tier IN ('Standard','VIP')"));
        builder
            .HasOne(b => b.Attendee)
            .WithOne(a => a.AttendeeBadge)
            .HasForeignKey<Badge>(b => b.AttendeeId);
    }
}