namespace BankManagementSystem.Domain.Models;

public sealed class Manager : BaseEntity
{
    /* Properties */
    public string Fullname { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public DateOnly HireDate { get; set; } = default!;
    public Branch Branch { get; set; } = default!; 
}
