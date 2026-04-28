using BankManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.DTOs.AccountCustomerDTOs;

public record AccountCustomerDetailsDTO
{
    public string AccountNumber { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public decimal AccountBalance { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public OwnershipType OwnershipType { get; set; }
    public AccountStatus AccountStatus { get; set; }
}
