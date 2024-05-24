using System.Net.Security;

namespace KallyPoker.Tests;

public class CardCollectionTests
{
    [Theory]
    [InlineData("TC", "TC")]
    [InlineData("TC,AH", "AH,TC")]
    [InlineData("TC,9C,AH", "AH,TC,9C")]
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
            "AS,KS,QS,JS,TS,9S,8S,7S,6S,5S,4S,3S,2S,AH,KH,QH,JH,TH,9H,8H,7H,6H,5H,4H,3H,2H,AD,KD,QD,JD,TD,9D,8D,7D,6D,5D,4D,3D,2D,AC,KC,QC,JC,TC,9C,8C,7C,6C,5C,4C,3C,2C",
            cardCollection.ToString());
    }
}