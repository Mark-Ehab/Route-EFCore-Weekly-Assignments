using BankManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.DTOs.AccountCustomerDTOs;

public sealed record UpdateAccountStatusDTO
{
    public string AccountNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public AccountStatus AccountStatus { get; set; }

}
