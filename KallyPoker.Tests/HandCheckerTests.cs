namespace KallyPoker.Tests;

public class HandCheckerTests
{
    [Theory]
    [InlineData("AC,KC,QC,JC,TC")]
    [InlineData("AD,KD,QD,JD,TD")]
    [InlineData("AH,KH,QH,JH,TH")]
    [InlineData("AS,KS,QS,JS,TS")]
    public void TestRoyalFlush(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var flush = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(flush.IsEmpty);
        Assert.Equal(HandRank.RoyalFlush, flush.Rank);
        Assert.Equal(expected, flush.Cards.ToString());
    }
    
    [Theory]
    [InlineData("KC,QC,JC,TC,9C")]
    [InlineData("QD,JD,TD,9D,8D")]
    [InlineData("JH,TH,9H,8H,7H")]
    [InlineData("TS,9S,8S,7S,6S")]
    [InlineData("9C,8C,7C,6C,5C")]
    [InlineData("8D,7D,6D,5D,4D")]
    [InlineData("7H,6H,5H,4H,3H")]
    [InlineData("6S,5S,4S,3S,2S")]
    [InlineData("5C,4C,3C,2C,AC")]
    [InlineData("AD,AH,QH,JH,TH,9H,8H", "QH,JH,TH,9H,8H")]
    public void TestStraightFlush(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var flush = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(flush.IsEmpty);
        Assert.Equal(HandRank.StraightFlush, flush.Rank);
        Assert.Equal(expected, flush.Cards.ToString());
    }
    
    [Theory]
    [InlineData("AC,AD,AH,AS,KC")]
    [InlineData("KC,KD,KH,KS,QC")]
    [InlineData("QC,QD,QH,QS,JC")]
    [InlineData("JC,JD,JH,JS,TC")]
    [InlineData("TC,TD,TH,TS,AC")]
    [InlineData("9C,9D,9H,9S,8C")]
    [InlineData("8C,8D,8H,8S,7C")]
    [InlineData("7C,7D,7H,7S,6C")]
    [InlineData("6C,6D,6H,6S,5C")]
    [InlineData("5C,5D,5H,5S,4C")]
    [InlineData("4C,4D,4H,4S,3C")]
    [InlineData("3C,3D,3H,3S,2C")]
    [InlineData("2C,2D,2H,2S,AC")]
    public void TestFourKind(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var fourKind = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(fourKind.IsEmpty);
        Assert.Equal(HandRank.FourKind, fourKind.Rank);
        Assert.Equal(expected, fourKind.Cards.ToString());
    }
    
    [Theory]
    [InlineData("AC,AD,AH,KC,KD")]
    [InlineData("9D,6C,6D,3C,2C,2D,2S", "2C,2D,2S,6C,6D")]
    public void TestFullHouse(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var fullHouse = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(fullHouse.IsEmpty);
        Assert.Equal(HandRank.FullHouse, fullHouse.Rank);
        Assert.Equal(expected, fullHouse.Cards.ToString());
    }
    
    [Theory]
    [InlineData("AC,QC,JC,5C,3C")]
    [InlineData("AD,KD,JH,9D,8D,5H,2D", "AD,KD,9D,8D,2D")]
    [InlineData("AD,AH,QH,8H,5H,4H,2H", "AH,QH,8H,5H,4H")]
    public void TestFlush(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var flush = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(flush.IsEmpty);
        Assert.Equal(HandRank.Flush, flush.Rank);
        Assert.Equal(expected, flush.Cards.ToString());
    }
    
    [Theory]
    [InlineData("AC,KS,QD,JH,TC")]
    [InlineData("5C,4S,3D,2H,AC")]
    public void TestStraight(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var straight = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(straight.IsEmpty);
        Assert.Equal(HandRank.Straight, straight.Rank);
        Assert.Equal(expected, straight.Cards.ToString());
    }
    
    [Theory]
    [InlineData("AC,AD,AS,QC,JH")]
    public void TestThreeKind(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var threeKind = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(threeKind.IsEmpty);
        Assert.Equal(HandRank.ThreeKind, threeKind.Rank);
        Assert.Equal(expected, threeKind.Cards.ToString());
    }
    
    [Theory]
    [InlineData("KC,KD,QC,QS,AH")]
    public void TestTwoPair(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var twoPair = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(twoPair.IsEmpty);
        Assert.Equal(HandRank.TwoPair, twoPair.Rank);
        Assert.Equal(expected, twoPair.Cards.ToString());
    }
    
    [Theory]
    [InlineData("2C,2D,AH,QC,JS")]
    [InlineData("AD,KD,QH,JC,JH,8D,5H", "JC,JH,AD,KD,QH")]
    [InlineData("AD,KD,JH,9S,8D,5H,5S", "5H,5S,AD,KD,JH")]
    public void TestPair(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var pair = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(pair.IsEmpty);
        Assert.Equal(HandRank.Pair, pair.Rank);
        Assert.Equal(expected, pair.Cards.ToString());
    }
    
    [Theory]
    [InlineData("AH,QC,JS,3D,2C")]
    [InlineData("AD,KD,JH,8D,5H,4D,3S", "AD,KD,JH,8D,5H")]
    [InlineData("AD,KD,JH,TC,8D,5H,2C", "AD,KD,JH,TC,8D")]
    public void TestHighCard(string input, string? expected = null)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var highCard = HandChecker.GetBestHand(cardCollection);
        expected ??= input;
        Assert.False(highCard.IsEmpty);
        Assert.Equal(HandRank.HighCard, highCard.Rank);
        Assert.Equal(expected, highCard.Cards.ToString());
    }
}