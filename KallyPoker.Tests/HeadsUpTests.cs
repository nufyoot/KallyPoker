namespace KallyPoker.Tests;

public class HeadsUpTests
{
    [Theory]
    [InlineData("3H,8C,4H,8H,4S", "KC,4C", "TH,2H", HandRank.FullHouse, HandRank.Flush, 5, 5, 5, 5, 70)]
    [InlineData("3H,8C,4H,8H,4S", "KC,4C", "8D,8S", HandRank.FullHouse, HandRank.FourKind, 5, 5, 5, 15, 280)]
    [InlineData("3H,8C,4H,8H,4S", "KC,4C", "AC,AS", HandRank.FullHouse, HandRank.TwoPair, 5, 5, 5, 15, 130)]
    public void CheckWinnings(string communityCardsStr, string dealerCardsStr, string playerCardsStr, HandRank dealerRank, HandRank playerRank, int anteAndOdds, int tripsPlus, int pocketBonus, int raise, int expectedWinnings)
    {
        var communityCards = CardCollection.Parse(communityCardsStr);
        var dealerCards = CardCollection.Parse(dealerCardsStr);
        var playerCards = CardCollection.Parse(playerCardsStr);
        var handResult = HeadsUpHoldem.CheckHand(communityCards, dealerCards, playerCards);
        var actualWinnings = HeadsUpHoldem.CalculateWin(handResult, new HeadsUpBet(anteAndOdds, tripsPlus, pocketBonus, raise));
        
        Assert.Equal(handResult.DealerHandResult.Rank, dealerRank);
        Assert.Equal(handResult.PlayerHandResult.Rank, playerRank);
        
        Assert.Equal(expectedWinnings, actualWinnings);
    }
}