using Assignment02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02.Configurations;

public class ProfilePageConfiguration : IEntityTypeConfiguration<ProfilePage>
{
    public void Configure(EntityTypeBuilder<ProfilePage> builder)
    {
        builder
            .HasKey(pp => new { pp.Id, pp.OrganizerId });
        builder
            .Property(pp => pp.Id)
            .UseIdentityColumn(1,1);
        builder
            .Property(pp => pp.ShortBiography)
            .HasMaxLength(500);
        builder
            .Property(pp => pp.LinkToPersonalSite)
            .HasMaxLength(400);
        builder
            .Property(pp => pp.LinkToCompanySite)
            .HasMaxLength(400);
    }
}