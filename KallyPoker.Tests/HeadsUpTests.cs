namespace KallyPoker.Tests;

public class HeadsUpTests
{
    [Theory]
    [InlineData("3H,8C,4H,8H,4S", "KC,4C", "TH,2H", HandRank.FullHouse, HandRank.Flush, 5, 5, 5, 5, 70)]
    [InlineData("3H,8C,4H,8H,4S", "KC,4C", "8D,8S", HandRank.FullHouse, HandRank.FourKind, 5, 5, 5, 15, 280)]
    [InlineData("3H,8C,4H,8H,4S", "KC,4C", "AC,AS", HandRank.FullHouse, HandRank.TwoPair, 5, 5, 5, 15, 130)]
    [InlineData("JC,JS,TD,7H,5S", "QC,7C", "AD,6H", HandRank.TwoPair, HandRank.Pair, 5, 5, 5, 5, 0)]
    [InlineData("KH,KS,8H,7H,7S", "JS,6S", "JC,6D", HandRank.TwoPair, HandRank.TwoPair, 5, 5, 5, 5, 15)]
    [InlineData("TH,9C,7S,5S,2D", "QD,3H", "7H,2H", HandRank.HighCard, HandRank.TwoPair, 5, 5, 5, 5, 15)]
    [InlineData("JS,9C,6D,4C,4S", "9D,5D", "9S,3S", HandRank.TwoPair, HandRank.TwoPair, 5, 5, 5, 5, 15)]
    [InlineData("QD,JH,7D,6D,4H", "AC,8C", "7S,4S", HandRank.HighCard, HandRank.TwoPair, 5, 5, 5, 5, 15)]
    [InlineData("TS,8C,8H,6D,4C", "4D,4H", "KC,JS", HandRank.FullHouse, HandRank.Pair, 5, 5, 5, 5, 0)]
    [InlineData("AH,AS,KC,QC,QS", "JC,3S", "5D,3D", HandRank.TwoPair, HandRank.TwoPair, 5, 5, 5, 5, 15)]
    [InlineData("AH,JS,9H,8C,7H", "AC,4C", "6H,2C", HandRank.Pair, HandRank.HighCard, 5, 5, 5, 5, 0)]
    [InlineData("KD,QS,TS,9C,3H", "6D,3S", "AS,8H", HandRank.Pair, HandRank.HighCard, 5, 5, 5, 5, 0)]
    [InlineData("AD,KD,9C,4H,2C", "TD,3C", "TC,7S", HandRank.HighCard, HandRank.HighCard, 5, 5, 5, 5, 15)]
    [InlineData("AD,AS,QD,5D,4D", "JH,4C", "TD,TS", HandRank.TwoPair, HandRank.Flush, 5, 5, 5, 5, 97.5)]
    [InlineData("AD,AS,QD,5D,4D", "KD,4C", "TD,TS", HandRank.Flush, HandRank.Flush, 5, 5, 5, 5, 100)]
    public void CheckWinnings(string communityCardsStr, string dealerCardsStr, string playerCardsStr, HandRank dealerRank, HandRank playerRank, decimal anteAndOdds, decimal tripsPlus, decimal pocketBonus, decimal raise, decimal expectedWinnings)
    {
        var communityCards = CardCollection.Parse(communityCardsStr);
        var dealerCards = CardCollection.Parse(dealerCardsStr);
        var playerCards = CardCollection.Parse(playerCardsStr);
        var handResult = HeadsUpHoldem.CheckHand(communityCards, dealerCards, playerCards);
        var actualWinnings = HeadsUpHoldem.CalculateWin(handResult, new HeadsUpBet(anteAndOdds, tripsPlus, pocketBonus, raise));
        
        Assert.Equal(dealerRank, handResult.DealerHandResult.Rank);
        Assert.Equal(playerRank, handResult.PlayerHandResult.Rank);
        
        Assert.Equal(expectedWinnings, actualWinnings);
    }
}