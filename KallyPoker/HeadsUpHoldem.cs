namespace KallyPoker;

public static class HeadsUpHoldem
{
    public static HeadsUpResult CheckHand(CardCollection communityCards, CardCollection dealerCards, CardCollection playerCards)
    {
        return new HeadsUpResult(communityCards, dealerCards, playerCards);
    }

    public static decimal CalculateOddsWin(HeadsUpResult result, HeadsUpBet bet)
    {
        if (bet.Raise == 0)
            return 0;
        
        return result.PlayerHandResult.Rank switch
        {
            HandRank.Straight => result.PlayerWins ? bet.Odds : 5 * bet.Odds,
            HandRank.Flush => result.PlayerWins ? 1.5m * bet.Odds : 6 * bet.Odds,
            HandRank.FullHouse => result.PlayerWins ? 4 * bet.Odds : 7 * bet.Odds,
            HandRank.FourKind => result.PlayerWins ? 11 * bet.Odds : 26 * bet.Odds,
            HandRank.StraightFlush => result.PlayerWins ? 51 * bet.Odds : 501 * bet.Odds,
            HandRank.RoyalFlush => 501 * bet.Odds,
            _ => bet.Odds,
        };
    }

    public static decimal CalculateWin(HeadsUpResult result, HeadsUpBet bet)
    {
        var total = 0m;

        // Figure out the Ante, Odds, and Raise payout, which only happens if the player raises (does not fold)
        if (bet.Raise > 0)
        {
            if (result.Tied)
                // If the player and dealer tied, it's a push on all 3
                total += bet.Ante + bet.Raise + bet.Odds;
            else
            {
                if (result.PlayerWins)
                    // If the player wins, they double up on the ante and raise
                    total += (2 * bet.Ante) + (2 * bet.Raise);
                
                // Figure out the Odds payout
            }
        }

        // Figure out the Odds payout, which only happens if the player entered a raise at some point
        
        if (bet.Raise > 0)
            
        
        // Figure out the Trips Plus payout
        total += result.PlayerHandResult.Rank switch
        {
            HandRank.ThreeKind => 4 * bet.TripsPlus,
            HandRank.Straight => 5 * bet.TripsPlus,
            HandRank.Flush => 8 * bet.TripsPlus,
            HandRank.FullHouse => 9 * bet.TripsPlus,
            HandRank.FourKind => 31 * bet.TripsPlus,
            HandRank.StraightFlush => 41 * bet.TripsPlus,
            HandRank.RoyalFlush => 101 * bet.TripsPlus,
            _ => 0,
        };
        
        // Finally, figure out the Pocket Bonus payout
        if (result.PlayerHasPocketAces)
            total += 26 * bet.PocketBonus;
        else if (result.PlayerHasAceFaceSuited)
            total += 21 * bet.PocketBonus;
        else if (result.PlayerHasAceFace)
            total += 11 * bet.PocketBonus;
        else if (result.PlayerHasPocketPair)
            total += 5 * bet.PocketBonus;

        return total;
    }
}