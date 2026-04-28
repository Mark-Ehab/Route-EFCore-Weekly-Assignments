using BankManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain.Models;

public sealed class Customer : BaseEntity
{
    /* Properties */
    public string FullName { get; set; } = String.Empty;
    public string NationalId { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public CustomerType Type { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public ICollection<AccountCustomer>? CustomerAccounts { get; set; } = new HashSet<AccountCustomer>();
}