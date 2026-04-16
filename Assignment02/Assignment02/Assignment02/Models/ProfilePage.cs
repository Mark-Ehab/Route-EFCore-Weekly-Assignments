using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment02.Models;

public sealed class ProfilePage
{
    /* Properties */
    public int Id { get; set; }
    public string? ShortBiography { get; set; }
    public string? LinkToPersonalSite { get; set; }
    public string? LinkToCompanySite { get; set; }
    public string? Logo { get; set; }
    public Organizer Organizer { get; set; } = default!;
    public int OrganizerId { get; set; }
}