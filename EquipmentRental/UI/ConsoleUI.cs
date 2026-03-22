namespace EquipmentRental.UI;

public static class ConsoleUI
{
    public static void Header(string title)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"══════════════════════════════════════");
        Console.WriteLine($"  {title}");
        Console.WriteLine($"══════════════════════════════════════");
        Console.ResetColor();
    }

    public static void Success(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  ✔  {message}");
        Console.ResetColor();
    }

    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  X  {message}");
        Console.ResetColor();
    }

    public static void Info(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  '  {message}");
        Console.ResetColor();
    }

    public static void PrintList<T>(IEnumerable<T> items)
    {
        foreach (var item in items)
            Console.WriteLine($"     * {item}");
    }

    public static void TryAction(string description, Action action)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  - {description}");
        Console.ResetColor();
        try
        {
            action();
        }
        catch (InvalidOperationException ex)
        {
            Error($"Blocked: {ex.Message}");
        }
    }
}