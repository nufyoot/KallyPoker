namespace KallyPoker;

public readonly ref struct ErrorTuple<T>
{
    public readonly Error Error;
    public readonly T? Result;
    public readonly bool HasError;

    private ErrorTuple(Error error)
    {
        Result = default;
        Error = error;
        HasError = true;
    }

    private ErrorTuple(T result)
    {
        Result = result;
        Error = default;
        HasError = false;
    }

    public static implicit operator T?(ErrorTuple<T> errorTuple) => errorTuple.Result;
    public static implicit operator Error(ErrorTuple<T> errorTuple) => errorTuple.Error;
    public static implicit operator ErrorTuple<T>(T result) => new(result);
    public static implicit operator ErrorTuple<T>(Error error) => new(error);
}