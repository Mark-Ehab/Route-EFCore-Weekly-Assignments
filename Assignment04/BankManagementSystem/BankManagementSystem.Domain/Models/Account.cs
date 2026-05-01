using BankManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain.Models;

public sealed class Account : BaseEntity
{
    /* Properties */
    public string Number { get; set; } = default!;
    public decimal Balance { get; set; }
    public AccountType Type { get; set; }
    public DateTime OpeningDate { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = default!;
    public ICollection<AccountCustomer> AccountCustomers { get; set; } = new HashSet<AccountCustomer>();
    public ICollection<Transaction>? Transactions { get; set; } = new HashSet<Transaction>();
}