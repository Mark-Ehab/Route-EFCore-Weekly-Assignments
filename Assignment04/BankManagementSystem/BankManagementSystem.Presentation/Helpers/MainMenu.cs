using BankManagementSystem.Application.Helpers;
using BankManagementSystem.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BankManagementSystem.Presentation.Helpers;

public static class MainMenu
{
    public static string Show()
    {
        ConsoleHelper.PrintHeader("National Bank - Management");
        ConsoleHelper.PrintLine(
                         """
                                 1) Add a new Customer
                                 2) Open a new Account for a Customer
                                 3) Update Account Status(Active / Closed)
                                 4) Remove an Account from a Customer
                                 5) List att Customers(with accounts)
                                 0) Exit
                                """,ConsoleColor.DarkCyan);
        ConsoleHelper.PrintLine(new string('-', 40));
        var option = ConsoleHelper.Prompt("choice");

        return option;
    }
}
