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
        Console.WriteLine("║                          ReadMore Books Store                      ║");
        Console.WriteLine("║                            Assiginment (1)                         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════════════╝\n");
        Console.ResetColor();
    }
}