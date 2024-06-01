using KallyPoker.PlayerTypes;

namespace KallyPoker.Tests;

public class HandResultTests
{
    [Theory]
    [InlineData(/* High Card  */ "AS,QH,JS,TC,9S", /* High Card  */ "AS,QH,TC,9D,6C", 1)]
    [InlineData(/* Pair       */ "8C,8D,QC,TH,9D", /* Two Pair   */ "TD,TH,8C,8D,9D", -1)]
    [InlineData(/* Pair       */ "8C,8D,QC,TH,9D", /* Pair       */ "8C,8D,JC,TH,9D", 1)]
    [InlineData(/* Pair       */ "8C,8D,QC,TH,9D", /* Pair       */ "8C,8D,QS,TH,9D", 0)]
    [InlineData(/* Pair       */ "8C,8D,QC,TH,9D", /* Pair       */ "8C,8D,QS,TH,8D", 1)]
    [InlineData(/* Two Pair   */ "TD,TH,8C,8D,9D", /* Straight   */ "TH,9D,8C,7H,6C", -1)]
    [InlineData(/* Straight   */ "JS,TH,9D,8C,7C", /* Straight   */ "TH,9D,8C,7H,6C", 1)]
    [InlineData(/* Flush      */ "JD,8D,7D,4D,2D", /* Flush      */ "8D,7D,5D,4D,2D", 1)]
    [InlineData(/* Full House */ "JC,JD,JS,8H,8S", /* Full House */ "8C,8H,8S,JC,JD", 1)]
    [InlineData(/* Full House */ "8D,8H,8S,JC,JD", /* Full House */ "8C,8H,8S,JC,JD", 0)]
    [InlineData(/* Three Kind */ "4C,4D,4S,KD,JD", /* Three Kind */ "4D,4H,4S,AS,KD", -1)]
    public void TestHandResultComparison(string hand1, string hand2, int comparison)
    {
        var cardCollection1 = CardCollection.Parse(hand1).ResultOrThrow;
        var cardCollection2 = CardCollection.Parse(hand2).ResultOrThrow;

        var handResult1 = HandChecker.GetBestHand(cardCollection1);
        var handResult2 = HandChecker.GetBestHand(cardCollection2);
        
        Assert.Equal(comparison, handResult1.CompareTo(handResult2));
    }

    [Theory]
    [InlineData("9D,8D,6C", "8C", "TH", "4", "TD,6H", "QC,5S", "AD,7H", "JS,7C", "JC,4D")]
    [InlineData("AH,8C,5S", "TH", "8D", "4", "7H,6D", "KH,JS", "KS,JC", "8H,6S", "7S,3D")]
    [InlineData("QH,JC,5S", "KH", "TC", "4,5", "6D,5C", "JH,TD", "9S,7S", "AD,7H", "AC,JD")]
    public void TestWinningHand(string flop, string turn, string river, string winningPlayerString, params string[] handStrings)
    {
        var table = new Table()
        {
            Flop = CardCollection.Parse(flop),
            Turn = CardCollection.Parse(turn),
            River = CardCollection.Parse(river)
        };

        for (var i = 0; i < 5; i++)
            table.Players[i] = new Player(i + 1, 100, new Caller(), CardCollection.Parse(handStrings[i]));

        var expectedWinningPlayers = winningPlayerString.Split(',').Select(int.Parse).ToArray();
        var winningHands = HandChecker.GetWinningHands(table);
        Assert.Equal(expectedWinningPlayers.Length, winningHands.Length);
        
        for (var i = 0; i < winningHands.Length; i++)
            Assert.Equal(expectedWinningPlayers[i], winningHands.Players[i].Id);
    }
}