using BankManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.DTOs.AccountCustomerDTOs;

public sealed record CustomerAccountsDTO
{
    public string CustomerFullName{ get; set; } = string.Empty;
    public CustomerType CustomerType { get; set; }
    public IEnumerable<AccountCustomerDetailsDTO> CustomerAccountsDetails { get; set; } = new HashSet<AccountCustomerDetailsDTO>();
}
