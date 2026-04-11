using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment01.Model;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? ShortBiography { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}