using BankManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain.Models;

public sealed class AccountCustomer : BaseEntity
{
    /* Properties */
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = default!;
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    public OwnershipType OwnershipType { get; set; }
    public AccountStatus AccountStatus { get; set; }
    public DateTime OwnershipStartDate { get; set; }
}