namespace KallyPoker;

public readonly ref struct HeadsUpResult(HandResult dealerHandResult, HandResult playerHandResult)
{
    public readonly HandResult DealerHandResult = dealerHandResult;
    public readonly HandResult PlayerHandResult = playerHandResult;
}