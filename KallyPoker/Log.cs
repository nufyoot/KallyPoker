using System.Diagnostics;

namespace KallyPoker;

public static class Log
{
    [Conditional("DEBUG")]
    public static void Info(string message)
    {
        Console.WriteLine(message);
    }
    
    [Conditional("DEBUG")]
    public static void Info(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}