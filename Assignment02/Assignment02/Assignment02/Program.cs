namespace Assignment02;

internal class Program
{
    private static void Main(string[] args)
    {
        PrintAssignmentIntro();
    }
    public static void PrintAssignmentIntro()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("╔════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                     EFCore - ASSIGNMENT WITH ANSWERS               ║");
        Console.WriteLine("║                              Event Hub                             ║");
        Console.WriteLine("║                            Assiginment (2)                         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();
    }
}
