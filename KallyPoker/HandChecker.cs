using System.Numerics;

namespace KallyPoker;

public static class HandChecker
{
    private static readonly ulong StraightAceHigh = Face.AcesMask | Face.KingsMask | Face.QueensMask | Face.JacksMask | Face.TensMask;
    
    public static CardCollection GetRoyalFlush(CardCollection cards)
    {
        var clubs = cards.Value & StraightAceHigh & Suit.Clubs.Mask;
        var diamonds = cards.Value & StraightAceHigh & Suit.Diamonds.Mask;
        var hearts = cards.Value & StraightAceHigh & Suit.Hearts.Mask;
        var spades = cards.Value & StraightAceHigh & Suit.Spades.Mask;

        if (BitOperations.PopCount(clubs) == 5)
            return new CardCollection(clubs);
        if (BitOperations.PopCount(diamonds) == 5)
            return new CardCollection(diamonds);
        if (BitOperations.PopCount(hearts) == 5)
            return new CardCollection(hearts);
        if (BitOperations.PopCount(spades) == 5)
            return new CardCollection(spades);

        return new CardCollection(0);
    }

    public static CardCollection GetStraightFlush(CardCollection cards)
    {
        
    }
}