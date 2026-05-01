using BankManagementSystem.Application.Contexts;
using BankManagementSystem.Application.Helpers;
using BankManagementSystem.Application.Seeder;
using BankManagementSystem.Application.Services;
using BankManagementSystem.Domain.Models;
using BankManagementSystem.Presentation.Helpers;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

/* Create an instance from BankSystemDbContext */
using var context = new ApplicationDbContext();

/* Create an instance from CustomerService */
var customerService = new CustomerService(context);

/* Create an instance from CustomerService */
var accountService = new AccountService(context);

/* Seed all the tables of database first */
await Seeder.SeedAllAsync(context);

while (true)
{
    /* Print the main menu */
    var option = MainMenu.Show();

    /* Check if passed option is null or empty */
    if (option.IsNullOrEmpty())
    {
        ConsoleHelper.PrintError("An option shall be entered !");

        ConsoleHelper.Prompt();

        ConsoleHelper.Clear();

        continue;
    }

    /* Parse the entered option */
    if (!byte.TryParse(option, out byte optionConverted))
    {
        ConsoleHelper.PrintError("Option format is invalid, Please enter a number from (0 - 5) !");

        ConsoleHelper.Prompt();

        ConsoleHelper.Clear();

        continue;
    }

    /* Parse the entered option */
    if (optionConverted is < 0 or > 5)
    {
        ConsoleHelper.PrintError("Option shall be with range (0 -> 5) !");

        ConsoleHelper.Prompt();

        ConsoleHelper.Clear();

        continue;
    }

    /* Check if user entered 0 to exit the program */
    if (optionConverted is 0)
    {
        ConsoleHelper.PrintLine();
        ConsoleHelper.PrintLine(new string('═', 26), ConsoleColor.DarkCyan);
        ConsoleHelper.PrintLine("\tGOOD BYE !", ConsoleColor.DarkGreen);
        ConsoleHelper.PrintLine(new string('═', 26), ConsoleColor.DarkCyan);
        break;
    }

    /* Clear the screen */
    ConsoleHelper.Clear();

    switch (option)
    {
        case "1":
            /* Add a new Customer */
            await customerService.AddNewCustomerAsync();
            break;
        case "2":
            /* Open a new Account for a Customer */
            await accountService.OpenNewAccountAsync();
            break;
        case "3":
            /* Update Account Status(Active / Closed) */
            await accountService.UpdateAccountStatusAsync();
            break;
        case "4":
            /* Remove an Account from a Customer */
            await accountService.RemoveAccountFromCustomerAsync();
            break;
        case "5":
            /* List att Customers(with accounts) */
            await customerService.ListAllCustomerWithAccountsAsync();
            break;
    }

    /* Prompt to the user to press any key to continue */
    ConsoleHelper.Prompt("any key to continue");

    /* Clear the screen */
    ConsoleHelper.Clear();

    continue;
}
