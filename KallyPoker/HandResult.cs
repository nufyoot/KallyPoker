using System.Collections;
using System.Runtime.CompilerServices;

namespace KallyPoker;

public readonly struct HandResult(HandRank rank, Hand cards)
{
    public static HandResult Empty => new(HandRank.Unknown, new Hand());
    
    public readonly Hand Cards = cards;
    public readonly HandRank Rank = rank;

    public bool IsEmpty => Rank == HandRank.Unknown;

    public ulong Score => (((ulong)Rank) << 16) | new CardCollection(Cards).FacesOnly().Bits;

    public static HandResult OneOf(Span<HandResult> results)
    {
        foreach (var result in results)
            if (!result.IsEmpty)
                return result;

        return Empty;
    }

    public int CompareTo(HandResult other)
    {
        if (Rank > other.Rank)
            return 1;
        if (Rank < other.Rank)
            return -1;

        switch (Rank)
        {
            case HandRank.RoyalFlush:
                // All royal flushes are equal. It shouldn't be possible to have multiple royal flushes
                // in a hand, but if it were, they would split the pot.
                return 0;
            
            case HandRank.StraightFlush:
                // Use the first card as the highest card in the hand. This works for hands like A2345 where
                // the 5 is the first card, not the Ace.
                return Cards[0].CompareTo(other.Cards[0]);
            
            case HandRank.FourKind:
                // First check the first card in the hand. It's certainly possible that there are two players
                // with a four of a kind, though unlikely. If there are, compare the 5th card.
                var firstCardComparison = Cards[0].CompareTo(other.Cards[0]);
                return firstCardComparison != 0 ? firstCardComparison : Cards[4].CompareTo(other.Cards[4]);

            default:
                return 0;
        }
    }
}