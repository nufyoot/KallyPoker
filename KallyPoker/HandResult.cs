using System.Runtime.CompilerServices;

namespace KallyPoker;

public readonly struct HandResult(HandRank rank, Hand cards)
{
    public static HandResult Empty => new(HandRank.Unknown, new Hand());
    
    public readonly Hand Cards = cards;
    public readonly HandRank Rank = rank;

    public bool IsEmpty => Rank == HandRank.Unknown;

    public static HandResult OneOf(Span<HandResult> results)
    {
        foreach (var result in results)
            if (!result.IsEmpty)
                return result;

        return Empty;
    }
}