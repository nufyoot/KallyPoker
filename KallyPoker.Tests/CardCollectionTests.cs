namespace KallyPoker.Tests;

public class CardCollectionTests
{
    [Theory]
    [InlineData("TC", "TC")]
    [InlineData("TC,AH", "AH,TC")]
    [InlineData("9C,AH,TC", "AH,TC,9C")]
    [InlineData("9C,AH", "AH,9C")]
    public void TestConstruction(string cards, string expected)
    {
        var cardCollection = CardCollection.Parse(cards);
        Assert.False(cardCollection.HasError);
        Assert.Equal(expected, cardCollection.Result.ToString());
    }

    [Fact]
    public void TestFullCardCollection()
    {
        var cardCollection = new CardCollection(Suit.ClubsMask | Suit.DiamondsMask | Suit.HeartsMask | Suit.SpadesMask);
        Assert.Equal(
            "AC,AD,AH,AS,KC,KD,KH,KS,QC,QD,QH,QS,JC,JD,JH,JS,TC,TD,TH,TS,9C,9D,9H,9S,8C,8D,8H,8S,7C,7D,7H,7S,6C,6D,6H,6S,5C,5D,5H,5S,4C,4D,4H,4S,3C,3D,3H,3S,2C,2D,2H,2S",
            cardCollection.ToString());
    }
}