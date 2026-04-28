using BankManagementSystem.Application.Contexts;
using BankManagementSystem.Application.DTOs.AccountCustomerDTOs;
using BankManagementSystem.Application.Extensions;
using BankManagementSystem.Application.Helpers;
using BankManagementSystem.Domain.Enums;
using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BankManagementSystem.Application.Services;

public class AccountService
{
    /* Fields */
    private readonly ApplicationDbContext _context;

    /* Constructor */
    public AccountService(ApplicationDbContext context)
    {
        _context = context;
    }

    /* Methods */
    public async Task OpenNewAccountAsync()
    {
        ConsoleHelper.PrintLine();
        ConsoleHelper.PrintSectionTitle("Open New Account");
        ConsoleHelper.PrintLine();

        var newAccountDetails = await PromptNewAccountDetailsAsync();

        ConsoleHelper.PrintLine($"Validating branch '{newAccountDetails.BranchCode}' and customer '{newAccountDetails.CustomerId}'...", ConsoleColor.Cyan);

        if (!await _context.Customers.AnyAsync(c => c.Id == newAccountDetails.CustomerId))
        {
            ConsoleHelper.PrintError("Customer Id doesn't exist !");
            return;
        }

        if (!await _context.Branches.AnyAsync(b => b.Code == newAccountDetails.BranchCode))
        {
            ConsoleHelper.PrintError("Branch doesn't exist !");
            return;
        }

        var branch = await _context
            .Branches
            .FirstOrDefaultAsync(b => b.Code == newAccountDetails.BranchCode);
    
        var newAccount = new Account
        {
            Number = newAccountDetails.AccountNumber,
            Type = newAccountDetails.AccountType,
            BranchId = branch!.Id
        };

        await _context.Accounts.AddAsync(newAccount);

        await _context.SaveChangesAsync();

        var newAccountCustomer = new AccountCustomer
        {
            CustomerId = newAccountDetails.CustomerId,
            AccountId = newAccount.Id,
            OwnershipStartDate = DateTime.UtcNow,
            OwnershipType = newAccountDetails.OwnershipRole,
            AccountStatus = AccountStatus.Active
        };

        await _context.AccountsCustomers.AddAsync(newAccountCustomer);

        await _context.SaveChangesAsync();

        ConsoleHelper.PrintSuccess($"""
                                            Account '{newAccount.Number}' created and linked to customer '{newAccountDetails.CustomerId}'
                                            as {newAccountDetails.OwnershipRole} owner.
                                           """);
    }
    public async Task UpdateAccountStatusAsync()
    {
        ConsoleHelper.PrintLine();
        ConsoleHelper.PrintSectionTitle("Update Account Status");
        ConsoleHelper.PrintLine();

        var accountStatusUpdateDetails = await PromptAccountStatusUpdateDetailsAsync();

        var account = await _context
            .Accounts
            .FirstOrDefaultAsync(a => a.Number == accountStatusUpdateDetails.AccountNumber);

        var accountCustomerToBeUpdated = await _context
            .AccountsCustomers
            .FirstOrDefaultAsync(ac => ac.CustomerId == accountStatusUpdateDetails.CustomerId && ac.AccountId == account!.Id);

        accountCustomerToBeUpdated!.AccountStatus = accountStatusUpdateDetails.AccountStatus;

        await _context.SaveChangesAsync();

        ConsoleHelper.PrintSuccess($"Status updated to {accountStatusUpdateDetails.AccountStatus}.");
    }
    public async Task RemoveAccountFromCustomerAsync()
    {
        ConsoleHelper.PrintLine();
        ConsoleHelper.PrintSectionTitle("Remove an Account from a Customer");
        ConsoleHelper.PrintLine();

        var customerRemovedAccountDetails = await PromptCustomerAccountRemovalDetailsAsync();

        var account = await _context
            .Accounts
            .FirstOrDefaultAsync(a => a.Number == customerRemovedAccountDetails.AccountNumber);

        var accountCustomerToBeRemoved = await _context
            .AccountsCustomers
            .FirstOrDefaultAsync(ac => ac.AccountId == account!.Id
                && customerRemovedAccountDetails.CustomerId == ac.CustomerId);

        _context.AccountsCustomers.Remove(accountCustomerToBeRemoved!);

        await _context.SaveChangesAsync();

        ConsoleHelper.PrintSuccess("Ownership link deleted.");

        if(!await _context.AccountsCustomers.AnyAsync(ac => ac.AccountId == account!.Id))
        {
            _context.Accounts.Remove(account!);

            await _context.SaveChangesAsync();

            ConsoleHelper.PrintLine($"That was the last owner — account '{account!.Number}' was also removed.",ConsoleColor.Cyan);
        }

    }
    private async Task<OpenNewAccountDetailsDTO> PromptNewAccountDetailsAsync()
    {
        string? accountNumber;
        do
        {
            accountNumber = ConsoleHelper.PromptEntry("Account Number");

            if (accountNumber.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Account Number is required !");
                continue;
            }

            if (accountNumber!.Length > 8)
            {
                ConsoleHelper.PrintError("Account Number maximum length is 8 characters !");
                continue;
            }

            if (await _context.Accounts.AnyAsync(a => a.Number == accountNumber))
            {
                ConsoleHelper.PrintError("Account Number already exists on the system !");
                continue;
            }

            break;
        }
        while (true);

        string? accountType;
        AccountType accountTypeConverted;
        do
        {
            accountType = ConsoleHelper.PromptEntry("Account Type", ["Savings", "Current", "Business"]);

            if (accountType.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Account Type is required !");
                continue;
            }

            if (Enum.TryParse(accountType, out accountTypeConverted) && !Enum.IsDefined(accountTypeConverted))
            {
                ConsoleHelper.PrintError(message: "Invalid account type please enter one the stated options !");
                continue;
            }

            break;
        }
        while (true);

        string? branchCode;
        do
        {
            branchCode = ConsoleHelper.PromptEntry("Branch Code");

            if (branchCode.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Branch Code is required !");
                continue;
            }

            if (branchCode!.Length > 7)
            {
                ConsoleHelper.PrintError("Branch Code maximum length is 7 characters !");
                continue;
            }

            break;
        }
        while (true);

        string? customerId;
        Guid customerIdConverted;
        do
        {
            customerId = ConsoleHelper.PromptEntry("Customer Id");

            if (customerId.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Customer Id is required !");
                continue;
            }

            if (!Guid.TryParse(customerId,out customerIdConverted))
            {
                ConsoleHelper.PrintError("Customer Id format is invalid !");
                continue;
            }

            break;
        }
        while (true);

        string? ownershipRole;
        OwnershipType ownershipRoleConverted;
        do
        {
            ownershipRole = ConsoleHelper.PromptEntry("Ownership Role", ["Primary", "CoHolder"]);

            if (ownershipRole.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Ownership Role is required !");
                continue;
            }

            if (Enum.TryParse(ownershipRole, out ownershipRoleConverted) && !Enum.IsDefined(ownershipRoleConverted))
            {
                ConsoleHelper.PrintError(message: "Invalid ownership role type please enter one the stated options !");
                continue;
            }

            break;
        }
        while (true);

        return new OpenNewAccountDetailsDTO
        {
            AccountNumber = accountNumber,
            AccountType = accountTypeConverted,
            BranchCode = branchCode,
            CustomerId = customerIdConverted,
            OwnershipRole = ownershipRoleConverted
        };
    }
    private async Task<UpdateAccountStatusDTO> PromptAccountStatusUpdateDetailsAsync()
    {
        string? accountNumber;
        do
        {
            accountNumber = ConsoleHelper.PromptEntry("Account Number");

            if (accountNumber.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Account Number is required !");
                continue;
            }

            if (accountNumber!.Length > 8)
            {
                ConsoleHelper.PrintError("Account Number maximum length is 8 characters !");
                continue;
            }

            if (!await _context.Accounts.AnyAsync(a => a.Number == accountNumber))
            {
                ConsoleHelper.PrintError("Account Number doesn't exist on the system !");
                continue;
            }

            break;
        }
        while (true);

        string? customerId;
        Guid customerIdConverted;
        do
        {
            customerId = ConsoleHelper.PromptEntry("Customer Id");

            if (customerId.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Customer Id is required !");
                continue;
            }

            if (!Guid.TryParse(customerId,out customerIdConverted))
            {
                ConsoleHelper.PrintError("Customer Id format is invalid !");
                continue;
            }

            if (!await _context.Customers.AnyAsync(c => c.Id == customerIdConverted))
            {
                ConsoleHelper.PrintError("Customer Id doesn't exist on the system !");
                continue;
            }

            break;
        }
        while (true);

        string? newAccoubtStatus;
        AccountStatus newAccoubtStatusConverted;
        do
        {
            newAccoubtStatus = ConsoleHelper.PromptEntry("Account Status", ["Active", "Closed"]);

            if (newAccoubtStatus.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Account Status is required !");
                continue;
            }

            if (Enum.TryParse(newAccoubtStatus, out newAccoubtStatusConverted) && !Enum.IsDefined(newAccoubtStatusConverted))
            {
                ConsoleHelper.PrintError(message: "Invalid account status type please enter one the stated options !");
                continue;
            }

            break;
        }
        while (true);

        return new UpdateAccountStatusDTO
        {
          AccountNumber = accountNumber,
          AccountStatus = newAccoubtStatusConverted,
          CustomerId = customerIdConverted
        };
    }
    private async Task<CustomerAccountRemovalDTO> PromptCustomerAccountRemovalDetailsAsync()
    {
        string? accountNumber;
        do
        {
            accountNumber = ConsoleHelper.PromptEntry("Account Number");

            if (accountNumber.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Account Number is required !");
                continue;
            }

            if (accountNumber!.Length > 8)
            {
                ConsoleHelper.PrintError("Account Number maximum length is 8 characters !");
                continue;
            }

            if (!await _context.Accounts.AnyAsync(a => a.Number == accountNumber))
            {
                ConsoleHelper.PrintError("Account Number doesn't exist on the system !");
                continue;
            }

            break;
        }
        while (true);

        string? customerId;
        Guid customerIdConverted;
        do
        {
            customerId = ConsoleHelper.PromptEntry("Customer Id");

            if (customerId.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Customer Id is required !");
                continue;
            }

            if (!Guid.TryParse(customerId,out customerIdConverted))
            {
                ConsoleHelper.PrintError("Customer Id format is invalid !");
                continue;
            }

            if (!await _context.Customers.AnyAsync(c => c.Id == customerIdConverted))
            {
                ConsoleHelper.PrintError("Customer Id doesn't exist on the system !");
                continue;
            }

            break;
        }
        while (true);

        return new CustomerAccountRemovalDTO
        {
          AccountNumber = accountNumber,
          CustomerId = customerIdConverted
        };
    }
}
