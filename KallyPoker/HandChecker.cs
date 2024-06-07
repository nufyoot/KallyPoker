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
        Span<HandResult> possibleHands = stackalloc HandResult[10]
        {
            GetRoyalFlush(cards),
            GetStraightFlush(cards),
            GetFourKind(cards),
            GetFullHouse(cards),
            GetFlush(cards),
            GetStraight(cards),
            GetThreeKind(cards),
            GetTwoPair(cards),
            GetPair(cards),
            GetHighCard(cards)
        };
        return HandResult.OneOf(possibleHands);
    }

    public static Winners GetWinningHands(PlayerCollection activePlayers, CardCollection communityCards)
    {
        Span<HandResult> allHandResults = stackalloc HandResult[PokerConstants.PlayerCount];
        var winners = new Winners();
        
        for (var i = 0; i < PokerConstants.PlayerCount; i++)
            allHandResults[i] = GetBestHand(CardCollection.Union(activePlayers[i].Cards, communityCards));

        winners.Players[0] = activePlayers[0];
        winners.Length = 1;
        var winningHand = allHandResults[0];

        for (var i = 1; i < PokerConstants.PlayerCount; i++)
        {
            switch (allHandResults[i].CompareTo(winningHand))
            {
                case -1:
                    // This hand isn't any better... skip it.
                    break;
                
                case 1:
                    // This is a better hand. Reset everything.
                    winners.Players[0] = activePlayers[i];
                    winners.Length = 1;
                    winningHand = allHandResults[i];
                    break;
                
                case 0:
                    winners.Players[winners.Length++] = activePlayers[i];
                    break;
            }
        }

        return winners;
    }
    
    private static HandResult GetRoyalFlush(CardCollection cards)
    {
        var clubs = cards.Filter(Suit.Clubs).Intersect(StraightAceHigh);
        var diamonds = cards.Filter(Suit.Diamonds).Intersect(StraightAceHigh);
        var hearts = cards.Filter(Suit.Hearts).Intersect(StraightAceHigh);
        var spades = cards.Filter(Suit.Spades).Intersect(StraightAceHigh);

        return
            clubs.Count == 5 ? new HandResult(HandRank.RoyalFlush, ConvertToHand(clubs)) :
            diamonds.Count == 5 ? new HandResult(HandRank.RoyalFlush, ConvertToHand(diamonds)) :
            hearts.Count == 5 ? new HandResult(HandRank.RoyalFlush, ConvertToHand(hearts)) :
            spades.Count == 5 ? new HandResult(HandRank.RoyalFlush, ConvertToHand(spades)) :
            HandResult.Empty;
    }

    private static HandResult GetStraightFlush(CardCollection cards)
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
                fiveHigh.Count == 5 ? new HandResult(HandRank.StraightFlush, new Hand(
                    new Card(suit, Face.Fives),
                    new Card(suit, Face.Fours),
                    new Card(suit, Face.Threes),
                    new Card(suit, Face.Twos),
                    new Card(suit, Face.Aces))) :
                HandResult.Empty;
        }
    }

    private static HandResult GetFourKind(CardCollection cards)
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
    
    private static HandResult GetFullHouse(CardCollection cards)
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

    private static HandResult GetFlush(CardCollection cards)
    {
        var clubs = cards.Filter(Suit.Clubs);
        var diamonds = cards.Filter(Suit.Diamonds);
        var hearts = cards.Filter(Suit.Hearts);
        var spades = cards.Filter(Suit.Spades);

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

    private static HandResult GetStraight(CardCollection cards)
    {
        var flattenedCards = cards.FacesOnly();

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
    
    private static HandResult GetThreeKind(CardCollection cards)
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

    private static HandResult GetTwoPair(CardCollection cards)
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
    
    private static HandResult GetPair(CardCollection cards)
    {
        // First, find the highest pair.
        var highestPair = GetFaceMatch(cards, 2);
        if (highestPair.IsEmpty)
            return HandResult.Empty;
        
        // Remove the highest pair and populate the rest of the cards
        var remainingCards = cards.Except(highestPair);
        var hand = new Hand();
        highestPair.CopyTo(hand, 2);
        remainingCards.CopyTo(hand[2..], 3);

        return new HandResult(HandRank.Pair, hand);
    }
    
    private static HandResult GetHighCard(CardCollection cards)
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