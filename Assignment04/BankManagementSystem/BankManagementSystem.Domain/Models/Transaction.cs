using BankManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain.Models;

public sealed class Transaction : BaseEntity
{
    /* Properties */
    public int Number { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = default!;
}