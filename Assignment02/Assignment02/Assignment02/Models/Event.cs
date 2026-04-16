using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Assignment02.Models;

[Table("Events")]
public sealed class Event
{
    /* Properties */
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(20)]
    public string Title { get; set; } = default!;
    [MaxLength(600)]
    public string DetailedDescription { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int MaxNumberOfAttendeesAllowed { get; set; }
    [InverseProperty(nameof(Organizer.Events))]
    public Organizer Organizer { get; set; } = default!;
    [ForeignKey(nameof(Organizer))]
    public int OrganizerId;
    [InverseProperty(nameof(EventAttendee.Event))]
    public ICollection<EventAttendee> EventAttendees { get; set; } = new HashSet<EventAttendee>();
    [InverseProperty(nameof(Sessions))]
    public Event? ParentEvent { get; set; }
    [InverseProperty(nameof(ParentEvent))]
    public ICollection<Event>? Sessions { get; set; }
    [ForeignKey(nameof(ParentEvent))]
    public int? ParentEventId { get; set; }
}