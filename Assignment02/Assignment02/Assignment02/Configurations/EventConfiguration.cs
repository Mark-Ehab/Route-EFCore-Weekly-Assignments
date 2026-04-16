using Assignment02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder
            .Property<DateTime>("CreatedAt")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();
        builder
            .Property<DateTime>("LastModifiedAt")
            .HasDefaultValueSql("SYSDATETIME()")
            .IsRequired();
    }
}