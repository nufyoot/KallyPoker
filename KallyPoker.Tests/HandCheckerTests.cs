namespace KallyPoker.Tests;

public class HandCheckerTests
{
    [Theory]
    [InlineData("AC,KC,QC,JC,TC")]
    [InlineData("AD,KD,QD,JD,TD")]
    [InlineData("AH,KH,QH,JH,TH")]
    [InlineData("AS,KS,QS,JS,TS")]
    public void TestRoyalFlush(string input)
    {
        var cardCollection = CardCollection.Parse(input).Result;
        var flush = HandChecker.GetRoyalFlush(cardCollection);
        Assert.False(flush.IsEmpty());
    }
}