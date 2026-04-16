using Assignment02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02.Configurations;

public sealed class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
{
    public void Configure(EntityTypeBuilder<Organizer> builder)
    {
        builder
            .HasKey(o => o.Id);
        builder
            .Property(o => o.Id)
            .UseIdentityColumn(1,1);
        builder
            .HasOne(o => o.ProfilePage)
            .WithOne(pp => pp.Organizer)
            .HasForeignKey<ProfilePage>(pp => pp.OrganizerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(o => o.Events)
            .WithOne(e => e.Organizer)
            .HasForeignKey(e => e.OrganizerId);
        builder
            .Property(o => o.Name)
            .HasMaxLength(50);
        builder
            .Property(o => o.CompanyName)
            .HasMaxLength(60);
    }
}