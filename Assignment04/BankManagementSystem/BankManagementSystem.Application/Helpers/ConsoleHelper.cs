using System.Collections;

namespace BankManagementSystem.Application.Helpers;

public static class ConsoleHelper
{
    public static void PrintHeader(string title)
    {
        int width = 40;
        Console.WriteLine(new string('═', width));
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(title.PadLeft((width + title.Length) / 2));
        Console.ResetColor();
        Console.WriteLine(new string('═', width));
    }
    public static void PrintSectionTitle(string title)
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;

        int width = 40;
        int sideWidth = (width - title.Length - 2) / 2;

        string side = new('─', sideWidth);

        Console.WriteLine($"{side} {title} {side}");

        Console.ResetColor();
    }
    public static void PrintOption(string option)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"  {option}");
        Console.ResetColor();
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{message}");
        Console.ResetColor();
    }

    public static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"{message}");
        Console.ResetColor();
    }

    public static void PrintWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{message}");
        Console.ResetColor();
    }
    public static string Prompt(string label = "")
    {
        string? value;

        do
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            if(!(label == string.Empty))
                Console.Write($"\nEnter {label} : ");

            Console.ForegroundColor = ConsoleColor.Green;
            value = Console.ReadLine()?.Trim();

            Console.ResetColor();

            if (string.IsNullOrWhiteSpace(value) && !(label == string.Empty))
            {
                PrintWarning($"{label} cannot be empty. Please try again.");
            }

            break;

        } while (true);

        return value!;
    }
    public static string? PromptEntry(string label, ICollection<string>? options = null)
    {
        string? value;

        Console.Write($"{label} : ");

        if (options is not null)
        {
            var counter = 0;

            Console.WriteLine();

            foreach (var option in options)
            {
                Console.WriteLine($"{++counter}) {option}");
            }

            Console.Write($"Choice: ");
        }

        value = Console.ReadLine()?.Trim();

        return value;
    }
    public static void PrintLine(string? message = null, ConsoleColor color = ConsoleColor.White)
    {
        if(message is null)
        {
            Console.WriteLine();
            return;
        }

        Console.ForegroundColor = color;
        Console.WriteLine($"{message}");
        Console.ResetColor();
    }
    public static void Print(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write($"{message}");
        Console.ResetColor();
    }
    public static void Clear()
    {
        Console.Clear();
    }
}
