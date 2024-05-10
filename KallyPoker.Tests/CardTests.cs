namespace KallyPoker.Tests;

public class CardTests
{
    [Fact]
    public void TestClubStrings()
    {
        Assert.Equal("2C", new Card(Suit.Clubs, Face.Twos).ToString());
        Assert.Equal("3C", new Card(Suit.Clubs, Face.Threes).ToString());
        Assert.Equal("4C", new Card(Suit.Clubs, Face.Fours).ToString());
        Assert.Equal("5C", new Card(Suit.Clubs, Face.Fives).ToString());
        Assert.Equal("6C", new Card(Suit.Clubs, Face.Sixes).ToString());
        Assert.Equal("7C", new Card(Suit.Clubs, Face.Sevens).ToString());
        Assert.Equal("8C", new Card(Suit.Clubs, Face.Eights).ToString());
        Assert.Equal("9C", new Card(Suit.Clubs, Face.Nines).ToString());
        Assert.Equal("TC", new Card(Suit.Clubs, Face.Tens).ToString());
        Assert.Equal("JC", new Card(Suit.Clubs, Face.Jacks).ToString());
        Assert.Equal("QC", new Card(Suit.Clubs, Face.Queens).ToString());
        Assert.Equal("KC", new Card(Suit.Clubs, Face.Kings).ToString());
        Assert.Equal("AC", new Card(Suit.Clubs, Face.Aces).ToString());
    }
    
    [Fact]
    public void TestDiamondStrings()
    {
        Assert.Equal("2D", new Card(Suit.Diamonds, Face.Twos).ToString());
        Assert.Equal("3D", new Card(Suit.Diamonds, Face.Threes).ToString());
        Assert.Equal("4D", new Card(Suit.Diamonds, Face.Fours).ToString());
        Assert.Equal("5D", new Card(Suit.Diamonds, Face.Fives).ToString());
        Assert.Equal("6D", new Card(Suit.Diamonds, Face.Sixes).ToString());
        Assert.Equal("7D", new Card(Suit.Diamonds, Face.Sevens).ToString());
        Assert.Equal("8D", new Card(Suit.Diamonds, Face.Eights).ToString());
        Assert.Equal("9D", new Card(Suit.Diamonds, Face.Nines).ToString());
        Assert.Equal("TD", new Card(Suit.Diamonds, Face.Tens).ToString());
        Assert.Equal("JD", new Card(Suit.Diamonds, Face.Jacks).ToString());
        Assert.Equal("QD", new Card(Suit.Diamonds, Face.Queens).ToString());
        Assert.Equal("KD", new Card(Suit.Diamonds, Face.Kings).ToString());
        Assert.Equal("AD", new Card(Suit.Diamonds, Face.Aces).ToString());
    }
    
    [Fact]
    public void TestHeartStrings()
    {
        Assert.Equal("2H", new Card(Suit.Hearts, Face.Twos).ToString());
        Assert.Equal("3H", new Card(Suit.Hearts, Face.Threes).ToString());
        Assert.Equal("4H", new Card(Suit.Hearts, Face.Fours).ToString());
        Assert.Equal("5H", new Card(Suit.Hearts, Face.Fives).ToString());
        Assert.Equal("6H", new Card(Suit.Hearts, Face.Sixes).ToString());
        Assert.Equal("7H", new Card(Suit.Hearts, Face.Sevens).ToString());
        Assert.Equal("8H", new Card(Suit.Hearts, Face.Eights).ToString());
        Assert.Equal("9H", new Card(Suit.Hearts, Face.Nines).ToString());
        Assert.Equal("TH", new Card(Suit.Hearts, Face.Tens).ToString());
        Assert.Equal("JH", new Card(Suit.Hearts, Face.Jacks).ToString());
        Assert.Equal("QH", new Card(Suit.Hearts, Face.Queens).ToString());
        Assert.Equal("KH", new Card(Suit.Hearts, Face.Kings).ToString());
        Assert.Equal("AH", new Card(Suit.Hearts, Face.Aces).ToString());
    }
    
    [Fact]
    public void TestSpaceStrings()
    {
        Assert.Equal("2S", new Card(Suit.Spades, Face.Twos).ToString());
        Assert.Equal("3S", new Card(Suit.Spades, Face.Threes).ToString());
        Assert.Equal("4S", new Card(Suit.Spades, Face.Fours).ToString());
        Assert.Equal("5S", new Card(Suit.Spades, Face.Fives).ToString());
        Assert.Equal("6S", new Card(Suit.Spades, Face.Sixes).ToString());
        Assert.Equal("7S", new Card(Suit.Spades, Face.Sevens).ToString());
        Assert.Equal("8S", new Card(Suit.Spades, Face.Eights).ToString());
        Assert.Equal("9S", new Card(Suit.Spades, Face.Nines).ToString());
        Assert.Equal("TS", new Card(Suit.Spades, Face.Tens).ToString());
        Assert.Equal("JS", new Card(Suit.Spades, Face.Jacks).ToString());
        Assert.Equal("QS", new Card(Suit.Spades, Face.Queens).ToString());
        Assert.Equal("KS", new Card(Suit.Spades, Face.Kings).ToString());
        Assert.Equal("AS", new Card(Suit.Spades, Face.Aces).ToString());
    }
}