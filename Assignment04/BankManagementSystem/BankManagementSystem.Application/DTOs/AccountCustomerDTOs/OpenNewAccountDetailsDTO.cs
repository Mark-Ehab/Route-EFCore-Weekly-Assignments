using BankManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.DTOs.AccountCustomerDTOs;

public record OpenNewAccountDetailsDTO
{
    public string AccountNumber { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public string BranchCode { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public OwnershipType OwnershipRole { get; set; }
}
