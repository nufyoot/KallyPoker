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
            case HandRank.Straight:
                // Use the first card as the highest card in the hand. This works for hands like A2345 where
                // the 5 is the first card, not the Ace.
                return Cards[0].CompareTo(other.Cards[0]);

            case HandRank.FourKind:
                // First check the first card in the hand. It's certainly possible that there are two players
                // with a four of a kind, though unlikely. If there are, compare the 5th card.
                var firstCardComparison = Cards[0].CompareTo(other.Cards[0]);
                return firstCardComparison != 0 ? firstCardComparison : Cards[4].CompareTo(other.Cards[4]);

            case HandRank.FullHouse:
                // Only two comparisons to do here. The first card and the fourth card.
                var fullHouseComparison = Cards[0].CompareTo(other.Cards[0]);
                return fullHouseComparison != 0 ? fullHouseComparison : Cards[3].CompareTo(other.Cards[3]);

            case HandRank.ThreeKind:
                var setComparison = Cards[0].CompareTo(other.Cards[0]);
                if (setComparison != 0)
                    return setComparison;
                
                // If the set matches, compare the kickers.
                for (var i = 3; i < 5; i++)
                {
                    var cardComparison = Cards[i].CompareTo(other.Cards[i]);
                    if (cardComparison != 0)
                        return cardComparison;
                }

                return 0;

            case HandRank.TwoPair:
                var firstPairComparison = Cards[0].CompareTo(other.Cards[0]);
                if (firstPairComparison != 0)
                    return firstPairComparison;

                var secondPairComparison = Cards[2].CompareTo(other.Cards[2]);
                return secondPairComparison != 0 ? secondPairComparison : Cards[4].CompareTo(other.Cards[4]);

            case HandRank.Pair:
                // Compare the first card, which is what was paired.
                var pairComparison = Cards[0].CompareTo(other.Cards[0]);
                if (pairComparison != 0)
                    return pairComparison;

                // If the pair matches, compare the kickers.
                for (var i = 2; i < 5; i++)
                {
                    var cardComparison = Cards[i].CompareTo(other.Cards[i]);
                    if (cardComparison != 0)
                        return cardComparison;
                }

                return 0;

            case HandRank.Flush:
            case HandRank.HighCard:
                // Compare each card, starting with the top card.
                for (var i = 0; i < 5; i++)
                {
                    var cardComparison = Cards[i].CompareTo(other.Cards[i]);
                    if (cardComparison != 0)
                        return cardComparison;
                }

                return 0;

            default:
                return 0;
        }
    }
}