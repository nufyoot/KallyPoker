using System.Collections;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace KallyPoker;

public static class HandChecker
{
    private static readonly CardCollection StraightAceHigh = new(Face.AcesMask | Face.KingsMask | Face.QueensMask | Face.JacksMask | Face.TensMask);
    private static readonly CardCollection StraightKingHigh = new(Face.KingsMask | Face.QueensMask | Face.JacksMask | Face.TensMask | Face.NinesMask);
    private static readonly CardCollection StraightQueenHigh = new(Face.QueensMask | Face.JacksMask | Face.TensMask | Face.NinesMask | Face.EightsMask);
    private static readonly CardCollection StraightJackHigh = new(Face.JacksMask | Face.TensMask | Face.NinesMask | Face.EightsMask | Face.SevensMask);
    private static readonly CardCollection StraightTenHigh = new(Face.TensMask | Face.NinesMask | Face.EightsMask | Face.SevensMask | Face.SixesMask);
    private static readonly CardCollection StraightNineHigh = new(Face.NinesMask | Face.EightsMask | Face.SevensMask | Face.SixesMask | Face.FivesMask);
    private static readonly CardCollection StraightEightHigh = new(Face.EightsMask | Face.SevensMask | Face.SixesMask | Face.FivesMask | Face.FoursMask);
    private static readonly CardCollection StraightSevenHigh = new(Face.SevensMask | Face.SixesMask | Face.FivesMask | Face.FoursMask | Face.ThreesMask);
    private static readonly CardCollection StraightSixHigh = new(Face.SixesMask | Face.FivesMask | Face.FoursMask | Face.ThreesMask | Face.TwosMask);
    private static readonly CardCollection StraightFiveHigh = new(Face.FivesMask | Face.FoursMask | Face.ThreesMask | Face.TwosMask | Face.AcesMask);

    public static HandResult GetBestHand(CardCollection cards)
    {
        var royalFlush = GetRoyalFlush(cards);
        if (!royalFlush.IsEmpty)
            return royalFlush;

        var straightFlush = GetStraightFlush(cards);
        if (!straightFlush.IsEmpty)
            return straightFlush;

        var fourKind = GetFourKind(cards);
        if (!fourKind.IsEmpty)
            return fourKind;

        var fullHouse = GetFullHouse(cards);
        if (!fullHouse.IsEmpty)
            return fullHouse;

        var flush = GetFlush(cards);
        if (!flush.IsEmpty)
            return flush;

        var straight = GetStraight(cards);
        if (!straight.IsEmpty)
            return straight;

        var threeKind = GetThreeKind(cards);
        if (!threeKind.IsEmpty)
            return threeKind;

        var twoPair = GetTwoPair(cards);
        if (!twoPair.IsEmpty)
            return twoPair;

        var pair = GetPair(cards);
        return !pair.IsEmpty ? pair : GetHighCard(cards);
    }
    
    public static HandResult GetRoyalFlush(CardCollection cards)
    {
        var clubs = cards.GetClubs().Intersect(StraightAceHigh);
        var diamonds = cards.GetDiamonds().Intersect(StraightAceHigh);
        var hearts = cards.GetHearts().Intersect(StraightAceHigh);
        var spades = cards.GetSpades().Intersect(StraightAceHigh);

        return
            clubs.Count == 5 ? new HandResult(HandRank.RoyalFlush, ConvertToHand(clubs)) :
            diamonds.Count == 5 ? new HandResult(HandRank.RoyalFlush, ConvertToHand(diamonds)) :
            hearts.Count == 5 ? new HandResult(HandRank.RoyalFlush, ConvertToHand(hearts)) :
            spades.Count == 5 ? new HandResult(HandRank.RoyalFlush, ConvertToHand(spades)) :
            HandResult.Empty;
    }

    public static HandResult GetStraightFlush(CardCollection cards)
    {
        var clubs = GetStraightFlushForSuit(Suit.Clubs);
        var diamonds = GetStraightFlushForSuit(Suit.Diamonds);
        var hearts = GetStraightFlushForSuit(Suit.Hearts);
        var spades = GetStraightFlushForSuit(Suit.Spades);

        return
            !clubs.IsEmpty ? clubs :
            !diamonds.IsEmpty ? diamonds :
            !hearts.IsEmpty ? hearts :
            !spades.IsEmpty ? spades :
            HandResult.Empty;
        
        HandResult GetStraightFlushForSuit(Suit suit)
        {
            var suitCards = cards.Filter(suit);
            
            // There is no check for StraightAceHigh because that would be a royal flush.
            var kingHigh = suitCards.Intersect(StraightKingHigh);
            var queenHigh = suitCards.Intersect(StraightQueenHigh);
            var jackHigh = suitCards.Intersect(StraightJackHigh);
            var tenHigh = suitCards.Intersect(StraightTenHigh);
            var nineHigh = suitCards.Intersect(StraightNineHigh);
            var eightHigh = suitCards.Intersect(StraightEightHigh);
            var sevenHigh = suitCards.Intersect(StraightSevenHigh);
            var sixHigh = suitCards.Intersect(StraightSixHigh);
            var fiveHigh = suitCards.Intersect(StraightFiveHigh);

            return
                kingHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, ConvertToHand(kingHigh)) :
                queenHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, ConvertToHand(queenHigh)) :
                jackHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, ConvertToHand(jackHigh)) :
                tenHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, ConvertToHand(tenHigh)) :
                nineHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, ConvertToHand(nineHigh)) :
                eightHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, ConvertToHand(eightHigh)) :
                sevenHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, ConvertToHand(sevenHigh)) :
                sixHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, ConvertToHand(sixHigh)) :
                fiveHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, new Hand((suit, Face.Fives), (suit, Face.Fours), (suit, Face.Threes), (suit, Face.Twos), (suit, Face.Aces))) :
                HandResult.Empty;
        }
    }

    public static HandResult GetFourKind(CardCollection cards)
    {
        var fourKind = GetFaceMatch(cards, 4);
        if (fourKind.IsEmpty)
            return HandResult.Empty;

        var hand = new Hand();
        
        // Copy over the four of a kind cards first.
        fourKind.CopyTo(hand, 4);
        
        // Figure out what cards are remaining and copy the top card over.
        var remainingCards = cards.Except(fourKind);
        remainingCards.CopyTo(hand[4..], 1);

        return new HandResult(HandRank.FourKind, hand);
    }
    
    public static HandResult GetFullHouse(CardCollection cards)
    {
        // First, find the highest three of a kind.
        var threeKind = GetFaceMatch(cards, 3);
        if (threeKind.IsEmpty)
            return HandResult.Empty;
        
        // Remove the three of a kind cards and find the highest pair
        var remainingCards = cards.Except(threeKind);
        var pair = GetFaceMatch(remainingCards, 2);
        if (pair.IsEmpty)
            return HandResult.Empty;
        
        // Now we have the highest three of a kind, and the highest pair, create a hand for this full house.
        var hand = new Hand();
        threeKind.CopyTo(hand, 3);
        pair.CopyTo(hand[3..], 2);

        return new HandResult(HandRank.FullHouse, hand);
    }

    public static HandResult GetFlush(CardCollection cards)
    {
        var clubs = cards.GetClubs();
        var diamonds = cards.GetDiamonds();
        var hearts = cards.GetHearts();
        var spades = cards.GetSpades();

        var hand = new Hand();
        if (clubs.Count >= 5)
            clubs.CopyTo(hand, 5);
        else if (diamonds.Count >= 5)
            diamonds.CopyTo(hand, 5);
        else if (hearts.Count >= 5)
            hearts.CopyTo(hand, 5);
        else if (spades.Count >= 5)
            spades.CopyTo(hand, 5);
        else
            return HandResult.Empty;

        return new HandResult(HandRank.Flush, hand);
    }

    public static HandResult GetStraight(CardCollection cards)
    {
        // First, flatten the hand to remove any notion of suits
        var clubs = cards.GetClubs();
        var diamonds = cards.GetDiamonds();
        var hearts = cards.GetHearts();
        var spades = cards.GetSpades();

        var flattenedCards = new CardCollection(
            (clubs.Value >> Suit.ClubsBitShift) |
            (diamonds.Value >> Suit.DiamondsBitShift) |
            (hearts.Value >> Suit.HeartsBitShift) |
            (spades.Value >> Suit.SpadesBitShift)
        );

        Hand hand;
        if (flattenedCards.Intersect(StraightAceHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Aces),
                cards.GetTopCard(Face.Kings),
                cards.GetTopCard(Face.Queens),
                cards.GetTopCard(Face.Jacks),
                cards.GetTopCard(Face.Tens));
        else if (flattenedCards.Intersect(StraightKingHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Kings),
                cards.GetTopCard(Face.Queens),
                cards.GetTopCard(Face.Jacks),
                cards.GetTopCard(Face.Tens),
                cards.GetTopCard(Face.Nines));
        else if (flattenedCards.Intersect(StraightQueenHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Queens),
                cards.GetTopCard(Face.Jacks),
                cards.GetTopCard(Face.Tens),
                cards.GetTopCard(Face.Nines),
                cards.GetTopCard(Face.Eights));
        else if (flattenedCards.Intersect(StraightJackHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Jacks),
                cards.GetTopCard(Face.Tens),
                cards.GetTopCard(Face.Nines),
                cards.GetTopCard(Face.Eights),
                cards.GetTopCard(Face.Sevens));
        else if (flattenedCards.Intersect(StraightTenHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Tens),
                cards.GetTopCard(Face.Nines),
                cards.GetTopCard(Face.Eights),
                cards.GetTopCard(Face.Sevens),
                cards.GetTopCard(Face.Sixes));
        else if (flattenedCards.Intersect(StraightNineHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Nines),
                cards.GetTopCard(Face.Eights),
                cards.GetTopCard(Face.Sevens),
                cards.GetTopCard(Face.Sixes),
                cards.GetTopCard(Face.Fives));
        else if (flattenedCards.Intersect(StraightEightHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Eights),
                cards.GetTopCard(Face.Sevens),
                cards.GetTopCard(Face.Sixes),
                cards.GetTopCard(Face.Fives),
                cards.GetTopCard(Face.Fours));
        else if (flattenedCards.Intersect(StraightSevenHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Sevens),
                cards.GetTopCard(Face.Sixes),
                cards.GetTopCard(Face.Fives),
                cards.GetTopCard(Face.Fours),
                cards.GetTopCard(Face.Threes));
        else if (flattenedCards.Intersect(StraightSixHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Sixes),
                cards.GetTopCard(Face.Fives),
                cards.GetTopCard(Face.Fours),
                cards.GetTopCard(Face.Threes),
                cards.GetTopCard(Face.Twos));
        else if (flattenedCards.Intersect(StraightFiveHigh).Count == 5)
            hand = new Hand(
                cards.GetTopCard(Face.Fives),
                cards.GetTopCard(Face.Fours),
                cards.GetTopCard(Face.Threes),
                cards.GetTopCard(Face.Twos),
                cards.GetTopCard(Face.Aces));
        else
            return HandResult.Empty;

        return new HandResult(HandRank.Straight, hand);
    }
    
    public static HandResult GetThreeKind(CardCollection cards)
    {
        var threeKind = GetFaceMatch(cards, 3);
        if (threeKind.IsEmpty)
            return HandResult.Empty;

        var hand = new Hand();
        
        // Copy over the three of a kind cards first.
        threeKind.CopyTo(hand, 3);
        
        // Figure out what cards are remaining and copy the top 2 cards over.
        var remainingCards = cards.Except(threeKind);
        remainingCards.CopyTo(hand[3..], 2);

        return new HandResult(HandRank.ThreeKind, hand);
    }

    public static HandResult GetTwoPair(CardCollection cards)
    {
        // First, find the highest pair.
        var highestPair = GetFaceMatch(cards, 2);
        if (highestPair.IsEmpty)
            return HandResult.Empty;
        
        // Remove the first pair and find the next highest pair
        var remainingCards = cards.Except(highestPair);
        var secondHighestPair = GetFaceMatch(remainingCards, 2);
        if (secondHighestPair.IsEmpty)
            return HandResult.Empty;
        
        // Now we have the two highest pairs, copy them over and find the next highest card
        remainingCards = remainingCards.Except(secondHighestPair);
        var hand = new Hand();
        highestPair.CopyTo(hand, 2);
        secondHighestPair.CopyTo(hand[2..], 2);
        hand[4] = remainingCards.GetTopCard();

        return new HandResult(HandRank.TwoPair, hand);
    }
    
    public static HandResult GetPair(CardCollection cards)
    {
        // First, find the highest pair.
        var highestPair = GetFaceMatch(cards, 2);
        if (highestPair.IsEmpty)
            return HandResult.Empty;
        
        // Remove the highest pair and fill the rest of the cards
        var remainingCards = cards.Except(highestPair);
        var hand = new Hand();
        highestPair.CopyTo(hand, 2);
        remainingCards.CopyTo(hand[2..], 3);

        return new HandResult(HandRank.Pair, hand);
    }
    
    public static HandResult GetHighCard(CardCollection cards)
    {
        var hand = new Hand();
        cards.CopyTo(hand, 5);
        return new HandResult(HandRank.HighCard, hand);
    }

    private static CardCollection GetFaceMatch(CardCollection cards, int matchCount)
    {
        var aces = cards.Filter(Face.Aces);
        var kings = cards.Filter(Face.Kings);
        var queens = cards.Filter(Face.Queens);
        var jacks = cards.Filter(Face.Jacks);
        var tens = cards.Filter(Face.Tens);
        var nines = cards.Filter(Face.Nines);
        var eights = cards.Filter(Face.Eights);
        var sevens = cards.Filter(Face.Sevens);
        var sixes = cards.Filter(Face.Sixes);
        var fives = cards.Filter(Face.Fives);
        var fours = cards.Filter(Face.Fours);
        var threes = cards.Filter(Face.Threes);
        var twos = cards.Filter(Face.Twos);

        return 
            aces.Count >= matchCount ? aces :
            kings.Count >= matchCount ? kings :
            queens.Count >= matchCount ? queens :
            jacks.Count >= matchCount ? jacks :
            tens.Count >= matchCount ? tens :
            nines.Count >= matchCount ? nines :
            eights.Count >= matchCount ? eights :
            sevens.Count >= matchCount ? sevens :
            sixes.Count >= matchCount ? sixes :
            fives.Count >= matchCount ? fives :
            fours.Count >= matchCount ? fours :
            threes.Count >= matchCount ? threes :
            twos.Count >= matchCount ? twos :
            CardCollection.Empty;
    }

    private static Hand ConvertToHand(CardCollection cards)
    {
        var hand = new Hand();
        cards.CopyTo(hand);
        return hand;
    }
}