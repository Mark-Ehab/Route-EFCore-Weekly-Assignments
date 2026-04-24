using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02.Models;

public sealed class Badge
{
    /* Properties */
    public int Id { get; set; }
    public DateOnly IssueDate { get; set; }
    public string Tier { get; set; } = default!;
    public Attendee Attendee { get; set; } = default!;
    public int AttendeeId { get; set; }
}