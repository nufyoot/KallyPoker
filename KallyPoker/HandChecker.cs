using System.Numerics;

namespace KallyPoker;

public static class HandChecker
{
    private static readonly CardCollection StraightAceHigh = new CardCollection(Face.AcesMask | Face.KingsMask | Face.QueensMask | Face.JacksMask | Face.TensMask);
    private static readonly CardCollection StraightKingHigh = new CardCollection(Face.KingsMask | Face.QueensMask | Face.JacksMask | Face.TensMask | Face.NinesMask);
    private static readonly CardCollection StraightQueenHigh = new CardCollection(Face.QueensMask | Face.JacksMask | Face.TensMask | Face.NinesMask | Face.EightsMask);
    private static readonly CardCollection StraightJackHigh = new CardCollection(Face.JacksMask | Face.TensMask | Face.NinesMask | Face.EightsMask | Face.SevensMask);
    private static readonly CardCollection StraightTenHigh = new CardCollection(Face.TensMask | Face.NinesMask | Face.EightsMask | Face.SevensMask | Face.SixesMask);
    private static readonly CardCollection StraightNineHigh = new CardCollection(Face.NinesMask | Face.EightsMask | Face.SevensMask | Face.SixesMask | Face.FivesMask);
    private static readonly CardCollection StraightEightHigh = new CardCollection(Face.EightsMask | Face.SevensMask | Face.SixesMask | Face.FivesMask | Face.FoursMask);
    private static readonly CardCollection StraightSevenHigh = new CardCollection(Face.SevensMask | Face.SixesMask | Face.FivesMask | Face.FoursMask | Face.ThreesMask);
    private static readonly CardCollection StraightSixHigh = new CardCollection(Face.SixesMask | Face.FivesMask | Face.FoursMask | Face.ThreesMask | Face.TwosMask);
    private static readonly CardCollection StraightFiveHigh = new CardCollection(Face.FivesMask | Face.FoursMask | Face.ThreesMask | Face.TwosMask | Face.AcesMask);
    
    public static CardCollection GetRoyalFlush(CardCollection cards)
    {
        var clubs = cards.GetClubs().Intersect(StraightAceHigh);
        var diamonds = cards.GetDiamonds().Intersect(StraightAceHigh);
        var hearts = cards.GetHearts().Intersect(StraightAceHigh);
        var spades = cards.GetSpades().Intersect(StraightAceHigh);

        if (BitOperations.PopCount(clubs.Value) == 5)
            return clubs;
        if (BitOperations.PopCount(diamonds.Value) == 5)
            return diamonds;
        if (BitOperations.PopCount(hearts.Value) == 5)
            return hearts;
        if (BitOperations.PopCount(spades.Value) == 5)
            return spades;

        return new CardCollection(0);
    }

    public static CardCollection GetStraightFlush(CardCollection cards)
    {
    }
}