using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Assignment02.Models;

public sealed class Attendee
{
    /* Properties */
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(100)]
    public string FullName { get; set; } = default!;
    [MaxLength(70)]
    public string EmailAddress { get; set; } = default!;
    [MaxLength(50)]
    public string? Street { get; set; } = default!;
    [MaxLength(30)]
    public string? City { get; set; } = default!;
    [MaxLength(30)]
    public string? Country { get; set; } = default!;
    [MaxLength(35)]
    public string? PostalCode { get; set; } = default!;
    [InverseProperty(nameof(EventAttendee.Attendee))]
    public ICollection<EventAttendee>? EventsAttendee { get; set; }
    [InverseProperty(nameof(Badge.Attendee))]
    public Badge? AttendeeBadge { get; set; }
}