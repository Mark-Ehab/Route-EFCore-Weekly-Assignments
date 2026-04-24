using Assignment02.Configurations;
using Assignment02.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02;

public sealed class ApplicationDbContext : DbContext
{
    /* DbSets */
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<ProfilePage> ProfilePages { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<EventAttendee> EventsAttendees { get; set; }
    public DbSet<Badge> Badges { get; set; }

    /* Methods */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=EventHubDb; Trusted_Connection=True; TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Apply configurations of Organizer and ProfilePage models using Fluent API configuration classes */
        modelBuilder.ApplyConfiguration(new OrganizerConfiguration());
        modelBuilder.ApplyConfiguration(new ProfilePageConfiguration());

        /* Configure EventAttendee Model using Fluent API through modelBuilder direct */
        modelBuilder
            .Entity<EventAttendee>()
            .HasKey(ea => new { ea.EventId, ea.AttendeeId });
        modelBuilder
            .Entity<EventAttendee>()
            .Property(ea => ea.ShortNote)
            .HasMaxLength(200);
        modelBuilder
            .Entity<EventAttendee>()
            .Property(ea => ea.RegisterationDate)
            .HasDefaultValueSql("SYSDATETIME()");
        modelBuilder
            .Entity<EventAttendee>()
            .HasOne(ea => ea.Event)
            .WithMany(e => e.EventAttendees)
            .HasForeignKey(ea => ea.EventId);
        modelBuilder
            .Entity<EventAttendee>()
            .HasOne(ea => ea.Attendee)
            .WithMany(a => a.EventsAttendee)
            .HasForeignKey(ea => ea.AttendeeId);

        /* Apply configurations of Event model using Fluent API configuration class */
        modelBuilder.ApplyConfiguration(new EventConfiguration());

        /* Apply configurations of Badge model using Fluent API configuration class */
        modelBuilder.ApplyConfiguration(new BadgeConfiguration());
    }
}