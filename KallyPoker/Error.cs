namespace KallyPoker;

public readonly ref struct Error(string message)
{
    public readonly string Message = message;
}