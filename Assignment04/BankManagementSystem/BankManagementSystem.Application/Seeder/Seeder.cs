using BankManagementSystem.Application.Contexts;
using BankManagementSystem.Domain.Enums;
using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagementSystem.Application.Seeder;

public sealed class Seeder
{
    async public static Task SeedAllAsync(ApplicationDbContext context)
    {
        /* Check if all table are already seeded */
        if (await context.Managers.AnyAsync()           || 
            await context.Branches.AnyAsync()           ||
            await context.Accounts.AnyAsync()           ||
            await context.Customers.AnyAsync()          ||
            await context.AccountsCustomers.AnyAsync()  ||
            await context.Transactions.AnyAsync())
        {
            return;
        }

        /* Managers */
        var managers = new List<Manager>
        {
            new() { Fullname = "Ahmed Hassan", Email = "ahmed@bank.com", PhoneNumber = "01000000001", HireDate = new DateOnly(2018,1,1) },
            new() { Fullname = "Sara Ali", Email = "sara@bank.com", PhoneNumber = "01000000002", HireDate = new DateOnly(2019,2,1) },
            new() { Fullname = "Omar Mahmoud", Email = "omar@bank.com", PhoneNumber = "01000000003", HireDate = new DateOnly(2020,3,1) },
            new() { Fullname = "Mona Adel", Email = "mona@bank.com", PhoneNumber = "01000000004", HireDate = new DateOnly(2021,4,1) },
            new() { Fullname = "Khaled Samy", Email = "khaled@bank.com", PhoneNumber = "01000000005", HireDate = new DateOnly(2022,5,1) }
        };

        await context.Managers.AddRangeAsync(managers);
        await context.SaveChangesAsync();

        /* Branches */
        var branches = new List<Branch>
        {
            new() { Code = "CAI-101", Name = "Cairo Main", Address = "Cairo", PhoneNumber = "022000001", ManagerId = managers[0].Id },
            new() { Code = "CAI-102", Name = "Giza", Address = "Giza", PhoneNumber = "022000002", ManagerId = managers[1].Id },
            new() { Code = "CAI-103", Name = "Alexandria", Address = "Alex", PhoneNumber = "022000003", ManagerId = managers[2].Id },
            new() { Code = "CAI-104", Name = "Nasr City", Address = "Nasr City", PhoneNumber = "022000004", ManagerId = managers[3].Id },
            new() { Code = "CAI-105", Name = "Maadi", Address = "Maadi", PhoneNumber = "022000005", ManagerId = managers[4].Id }
        };

        await context.Branches.AddRangeAsync(branches);
        await context.SaveChangesAsync();

        /* Customers */
        var customers = new List<Customer>
        {
            new() { FullName = "Mohamed Ali", NationalId = "11111111111111", Email = "c1@bank.com", Address = "Cairo", PhoneNumber = "01100000001", Type = CustomerType.Individual, DateOfBirth = new DateOnly(1990,1,1) },
            new() { FullName = "Omar Khaled", NationalId = "22222222222222", Email = "c2@bank.com", Address = "Giza", PhoneNumber = "01100000002", Type = CustomerType.Individual, DateOfBirth = new DateOnly(1992,2,2) },
            new() { FullName = "Sara Mohamed", NationalId = "33333333333333", Email = "c3@bank.com", Address = "Alex", PhoneNumber = "01100000003", Type = CustomerType.Individual, DateOfBirth = new DateOnly(1994,3,3) },
            new() { FullName = "Ali Hassan", NationalId = "44444444444444", Email = "c4@bank.com", Address = "Cairo", PhoneNumber = "01100000004", Type = CustomerType.Business, DateOfBirth = new DateOnly(1985,4,4) },
            new() { FullName = "Mona Adel", NationalId = "55555555555555", Email = "c5@bank.com", Address = "Maadi", PhoneNumber = "01100000005", Type = CustomerType.Individual, DateOfBirth = new DateOnly(1996,5,5) }
        };

        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();

        /* Accounts */
        var accounts = new List<Account>
        {
            new() { Number = "1001-SAV", Balance = 5000, Type = AccountType.Savings, OpeningDate = DateTime.UtcNow.AddYears(-3), BranchId = branches[0].Id },
            new() { Number = "1001-CUR", Balance = 8000, Type = AccountType.Current, OpeningDate = DateTime.UtcNow.AddYears(-2), BranchId = branches[1].Id },
            new() { Number = "1001-BUS", Balance = 12000, Type = AccountType.Business, OpeningDate = DateTime.UtcNow.AddYears(-1), BranchId = branches[2].Id },
            new() { Number = "1002-CUR", Balance = 3000, Type = AccountType.Current, OpeningDate = DateTime.UtcNow.AddMonths(-10), BranchId = branches[3].Id },
            new() { Number = "1002-SAV", Balance = 15000, Type = AccountType.Savings, OpeningDate = DateTime.UtcNow.AddMonths(-6), BranchId = branches[4].Id }
        };

        await context.Accounts.AddRangeAsync(accounts);
        await context.SaveChangesAsync();

        /* AccountsCustomers */
        var accountCustomers = new List<AccountCustomer>
        {
            new() { AccountId = accounts[0].Id, CustomerId = customers[0].Id, OwnershipType = OwnershipType.PrimaryHolder, AccountStatus = AccountStatus.Active, OwnershipStartDate = DateTime.UtcNow.AddYears(-3) },
            new() { AccountId = accounts[1].Id, CustomerId = customers[1].Id, OwnershipType = OwnershipType.PrimaryHolder, AccountStatus = AccountStatus.Active, OwnershipStartDate = DateTime.UtcNow.AddYears(-2) },
            new() { AccountId = accounts[2].Id, CustomerId = customers[3].Id, OwnershipType = OwnershipType.PrimaryHolder, AccountStatus = AccountStatus.Active, OwnershipStartDate = DateTime.UtcNow.AddYears(-1) },
            new() { AccountId = accounts[3].Id, CustomerId = customers[2].Id, OwnershipType = OwnershipType.PrimaryHolder, AccountStatus = AccountStatus.Active, OwnershipStartDate = DateTime.UtcNow.AddMonths(-10) },
            new() { AccountId = accounts[4].Id, CustomerId = customers[4].Id, OwnershipType = OwnershipType.PrimaryHolder, AccountStatus = AccountStatus.Active, OwnershipStartDate = DateTime.UtcNow.AddMonths(-6) },
            new() { AccountId = accounts[0].Id, CustomerId = customers[1].Id, OwnershipType = OwnershipType.CoHolder, AccountStatus = AccountStatus.Active, OwnershipStartDate = DateTime.UtcNow.AddYears(-1) }
        };

        await context.AccountsCustomers.AddRangeAsync(accountCustomers);
        await context.SaveChangesAsync();

        /* Tranactions */
        var transactions = new List<Transaction>
        {
            new() { Number = 1, Amount = 1000, Type = TransactionType.Deposit, Date = DateTime.UtcNow.AddMonths(-5), AccountId = accounts[0].Id, Note = "Initial deposit" },
            new() { Number = 2, Amount = 200, Type = TransactionType.Withdrawal, Date = DateTime.UtcNow.AddMonths(-4), AccountId = accounts[0].Id, Note = "ATM withdrawal" },
            new() { Number = 3, Amount = 5000, Type = TransactionType.Deposit, Date = DateTime.UtcNow.AddMonths(-3), AccountId = accounts[1].Id, Note = "Salary" },
            new() { Number = 4, Amount = 1500, Type = TransactionType.Payment, Date = DateTime.UtcNow.AddMonths(-2), AccountId = accounts[2].Id, Note = "Online purchase" },
            new() { Number = 5, Amount = 700, Type = TransactionType.Transfer, Date = DateTime.UtcNow.AddMonths(-1), AccountId = accounts[3].Id, Note = "Transfer" }
        };

        await context.Transactions.AddRangeAsync(transactions);
        await context.SaveChangesAsync();

        return;
    }
}