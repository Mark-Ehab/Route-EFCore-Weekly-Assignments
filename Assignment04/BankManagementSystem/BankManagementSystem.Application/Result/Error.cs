namespace BankManagementSystem.Application.Result;

public sealed record Error(string Code, string Description)
{
    public static Error None 
        => new ("String.Empty", "String is empty !");
}