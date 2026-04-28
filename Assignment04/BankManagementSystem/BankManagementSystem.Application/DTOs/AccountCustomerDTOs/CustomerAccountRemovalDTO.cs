using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.DTOs.AccountCustomerDTOs;

public record CustomerAccountRemovalDTO
{
    public string AccountNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
}
