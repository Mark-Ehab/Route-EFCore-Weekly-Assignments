using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02.Models;

public sealed class Organizer
{
    /* Properties */
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? CompanyName { get; set; }
    public bool IsVerified { get; set; }
    public ProfilePage ProfilePage { get; set; } = default!;
    public ICollection<Event>? Events { get; set; }
}