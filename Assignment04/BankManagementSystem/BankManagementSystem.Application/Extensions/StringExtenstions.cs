using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BankManagementSystem.Application.Extensions;

public static class StringExtenstions
{
    public static bool ValidateEmail(this string email)
        => Regex.IsMatch(email, @"(([^<>()\[\]\\.,;:\s@""]+(\.[^<>()\[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))");
    public static bool ValidatePhone(this string phone)
        => Regex.IsMatch(phone, @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$");
}