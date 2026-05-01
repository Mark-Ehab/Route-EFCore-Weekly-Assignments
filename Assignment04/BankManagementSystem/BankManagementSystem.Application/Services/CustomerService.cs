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
using System.Text;

namespace BankManagementSystem.Application.Services;

public sealed class CustomerService
{
    /* Fields */
    private readonly ApplicationDbContext _context;

    /* Constructor */
    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    /* Methods */
    public async Task AddNewCustomerAsync()
    {
        ConsoleHelper.PrintLine();
        ConsoleHelper.PrintSectionTitle("Add New Customer");
        ConsoleHelper.PrintLine();

        var newCustomer = await this.PromptCustomerDataAsync();

        await _context.Customers.AddAsync(newCustomer);

        await _context.SaveChangesAsync();

        var newCustomerId = newCustomer.Id;

        ConsoleHelper.PrintSuccess($"""
                                            Customer created successfully.
                                            CustomerId => {newCustomerId}
                                           """);
    }
    public async Task ListAllCustomerWithAccountsAsync()
    {
        var customersAccounts = await _context
            .Customers
            .AsSplitQuery()
            .Select(c => new CustomerAccountsDTO
            {
                CustomerFullName = c.FullName,
                CustomerType = c.Type,
                CustomerAccountsDetails = c.CustomerAccounts
                    .Select(ca => new AccountCustomerDetailsDTO
                    { 
                        AccountNumber = ca.Account.Number,
                        AccountType = ca.Account.Type,
                        AccountBalance = ca.Account.Balance,
                        BranchName = ca.Account.Branch.Name,
                        OwnershipType = ca.OwnershipType,
                        AccountStatus = ca.AccountStatus
                    })
            }
            ).ToListAsync();

        if(customersAccounts is null)
        {
            return;
        }

        PrintCustomerAccountsDetails(customersAccounts);
    }

    private async Task<Customer> PromptCustomerDataAsync()
    {
        string? fullName;
        do
        {
            fullName = ConsoleHelper.PromptEntry("FullName");

            if (fullName.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Fullname is required !");
                continue;
            }

            if (fullName!.Length > 40)
            {
                ConsoleHelper.PrintError("Fullname maximum length is 40 characters !");
                continue;
            }

            break;
        }
        while (true);

        string? nationalId;
        do
        {
            nationalId = ConsoleHelper.PromptEntry("National Id");

            if (nationalId.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("National Id is required !");
                continue;
            }

            if (nationalId!.Length != 20)
            {
                ConsoleHelper.PrintError("National Id shall be 20 characters !");
                continue;
            }

            if (await _context.Customers.AnyAsync(c => c.NationalId == nationalId))
            {
                ConsoleHelper.PrintError("National Id already exists on the system !");
                continue;
            }

            break;
        }
        while (true);

        string? dateOfBirth;
        DateOnly dateOfBirthConverted;
        do
        {
            dateOfBirth = ConsoleHelper.PromptEntry("Date of Birth (yyyy-MM-dd)");

            if (dateOfBirth.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Date of Birth is required !");
                continue;
            }

            if (!DateOnly.TryParse(dateOfBirth, out dateOfBirthConverted))
            {
                ConsoleHelper.PrintError("Format of Date of Birth is invalid !");
                continue;
            }

            break;
        }
        while (true);

        string? email;
        do
        {
            email = ConsoleHelper.PromptEntry("Email");

            if (email.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Email is required !");
                continue;
            }

            if (email!.Length > 80)
            {
                ConsoleHelper.PrintError("Email shall not exceed 80 characters !");
                continue;
            }

            if (!email!.ValidateEmail())
            {
                ConsoleHelper.PrintError("Email format is invalid !");
                continue;
            }

            if (await _context.Customers.AnyAsync(c => c.Email == email))
            {
                ConsoleHelper.PrintError("Email already exists on the system !");
                continue;
            }

            break;
        }
        while (true);

        string? phone;
        do
        {
            phone = ConsoleHelper.PromptEntry("Phone");

            if (phone.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Phone is required !");
                continue;
            }

            if (!phone!.ValidatePhone())
            {
                ConsoleHelper.PrintError("Phone format is invalid !");
                continue;
            }

            if (await _context.Customers.AnyAsync(c => c.PhoneNumber == phone))
            {
                ConsoleHelper.PrintError("Phone number already exists and belongs to other user on the system !");
                continue;
            }

            break;
        }
        while (true);

        string? address;
        do
        {
            address = ConsoleHelper.PromptEntry("Address");

            if (address.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Address is required !");
                continue;
            }

            if (email!.Length > 100)
            {
                ConsoleHelper.PrintError("Address shall not exceed 100 characters !");
                continue;
            }

            break;
        }
        while (true);

        string? customerType;
        CustomerType customerTypeConverted;
        do
        {
            customerType = ConsoleHelper.PromptEntry("Address", ["Individual","Business"]);

            if (customerType.IsNullOrEmpty())
            {
                ConsoleHelper.PrintError("Address is required !");
                continue;
            }

            if (Enum.TryParse(customerType,out customerTypeConverted) && !Enum.IsDefined(customerTypeConverted))
            {
                ConsoleHelper.PrintError(message: "Invalid customer type please enter one the stated options !");
                continue;
            }

            break;
        }
        while (true);

        return new Customer
        {
            FullName = fullName,
            NationalId = nationalId,
            DateOfBirth = dateOfBirthConverted,
            Email = email,
            PhoneNumber = phone,
            Address = address,
            Type = customerTypeConverted
        };
    }

    private void PrintCustomerAccountsDetails(IEnumerable<CustomerAccountsDTO> customersAccounts)
    {
        var customerCounter = 0;

        ConsoleHelper.PrintLine();
        ConsoleHelper.PrintSectionTitle("All Customers");
        ConsoleHelper.PrintLine();

        foreach (var customerAccounts in customersAccounts)
        {
            ConsoleHelper
                .PrintLine($"#{++customerCounter} {customerAccounts.CustomerFullName} ({customerAccounts.CustomerType})");

            if (!customerAccounts.CustomerAccountsDetails.Any())
            {
                ConsoleHelper
                    .PrintLine("    (No Accounts)");
            }

            foreach (var AccountDetails in customerAccounts.CustomerAccountsDetails)
            {
                ConsoleHelper
                    .PrintLine($"   {AccountDetails.AccountNumber} {AccountDetails.AccountType} " +
                    $"Balance: {AccountDetails.AccountBalance:F2} {AccountDetails.OwnershipType} " +
                    $"{AccountDetails.AccountStatus} @ {AccountDetails.BranchName} Branch.");
            }

            ConsoleHelper.PrintLine();
        }
    }
}
