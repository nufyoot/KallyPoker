namespace KallyPoker.Tests;

public class CardTests
{
    [Theory]
    [InlineData("clubs", Face.TwosMask, "2C")]
    [InlineData("clubs", Face.ThreesMask, "3C")]
    [InlineData("clubs", Face.FoursMask, "4C")]
    [InlineData("clubs", Face.FivesMask, "5C")]
    [InlineData("clubs", Face.SixesMask, "6C")]
    [InlineData("clubs", Face.SevensMask, "7C")]
    [InlineData("clubs", Face.EightsMask, "8C")]
    [InlineData("clubs", Face.NinesMask, "9C")]
    [InlineData("clubs", Face.TensMask, "TC")]
    [InlineData("clubs", Face.JacksMask, "JC")]
    [InlineData("clubs", Face.QueensMask, "QC")]
    [InlineData("clubs", Face.KingsMask, "KC")]
    [InlineData("clubs", Face.AcesMask, "AC")]
    [InlineData("diamonds", Face.TwosMask, "2D")]
    [InlineData("diamonds", Face.ThreesMask, "3D")]
    [InlineData("diamonds", Face.FoursMask, "4D")]
    [InlineData("diamonds", Face.FivesMask, "5D")]
    [InlineData("diamonds", Face.SixesMask, "6D")]
    [InlineData("diamonds", Face.SevensMask, "7D")]
    [InlineData("diamonds", Face.EightsMask, "8D")]
    [InlineData("diamonds", Face.NinesMask, "9D")]
    [InlineData("diamonds", Face.TensMask, "TD")]
    [InlineData("diamonds", Face.JacksMask, "JD")]
    [InlineData("diamonds", Face.QueensMask, "QD")]
    [InlineData("diamonds", Face.KingsMask, "KD")]
    [InlineData("diamonds", Face.AcesMask, "AD")]
    [InlineData("hearts", Face.TwosMask, "2H")]
    [InlineData("hearts", Face.ThreesMask, "3H")]
    [InlineData("hearts", Face.FoursMask, "4H")]
    [InlineData("hearts", Face.FivesMask, "5H")]
    [InlineData("hearts", Face.SixesMask, "6H")]
    [InlineData("hearts", Face.SevensMask, "7H")]
    [InlineData("hearts", Face.EightsMask, "8H")]
    [InlineData("hearts", Face.NinesMask, "9H")]
    [InlineData("hearts", Face.TensMask, "TH")]
    [InlineData("hearts", Face.JacksMask, "JH")]
    [InlineData("hearts", Face.QueensMask, "QH")]
    [InlineData("hearts", Face.KingsMask, "KH")]
    [InlineData("hearts", Face.AcesMask, "AH")]
    [InlineData("spades", Face.TwosMask, "2S")]
    [InlineData("spades", Face.ThreesMask, "3S")]
    [InlineData("spades", Face.FoursMask, "4S")]
    [InlineData("spades", Face.FivesMask, "5S")]
    [InlineData("spades", Face.SixesMask, "6S")]
    [InlineData("spades", Face.SevensMask, "7S")]
    [InlineData("spades", Face.EightsMask, "8S")]
    [InlineData("spades", Face.NinesMask, "9S")]
    [InlineData("spades", Face.TensMask, "TS")]
    [InlineData("spades", Face.JacksMask, "JS")]
    [InlineData("spades", Face.QueensMask, "QS")]
    [InlineData("spades", Face.KingsMask, "KS")]
    [InlineData("spades", Face.AcesMask, "AS")]
    public void TestStrings(string suitName, ulong faceMask, string expected)
    {
        var suit = suitName switch
        {
            "clubs" => Suit.Clubs,
            "diamonds" => Suit.Diamonds,
            "hearts" => Suit.Hearts,
            "spades" => Suit.Spades,
            _ => throw new NotImplementedException()
        };
        var actual = new Card(suit, new Face(faceMask)).ToString();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("AC", "KC", 1)]
    [InlineData("QC", "KC", -1)]
    [InlineData("QC", "QS", 0)]
    [InlineData("AC", "2S", 1)]
    public void TestComparisons(string firstCardString, string secondCardString, int comparison)
    {
        var firstCard = Card.Parse(firstCardString).Result;
        var secondCard = Card.Parse(secondCardString).Result;
        
        Assert.Equal(comparison, firstCard.CompareTo(secondCard));
    }
}