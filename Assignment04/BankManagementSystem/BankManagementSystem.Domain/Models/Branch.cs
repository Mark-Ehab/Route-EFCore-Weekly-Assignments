using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Domain.Models;

public sealed class Branch : BaseEntity
{
    /* Properties */
    public string Code { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Address { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public Guid ManagerId { get; set; }
    public Manager Manager { get; set; } = default!;
    public ICollection<Account>? Accounts { get; set; } = new HashSet<Account>();
}