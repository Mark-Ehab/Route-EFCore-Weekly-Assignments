using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02.Models;

public sealed class EventAttendee
{
    /* Properties */
    public int EventId { get; set; }
    public Event Event { get; set; } = default!;
    public int AttendeeId { get; set; }
    public Attendee Attendee { get; set; } = default!;
    public string? ShortNote { get; set; } = default!;
    public DateTime RegisterationDate { get; set; }
}