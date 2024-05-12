using System.Diagnostics;

namespace KallyPoker;

public readonly struct Card
{
    public readonly ulong Value;

    public Card(ulong value)
    {
        Value = value;
    }

    public Card(Suit suit, Face face)
    {
        Value = suit & face;
    }

    public Card(ReadOnlySpan<char> value)
    {
        if (value.Length != 2)
            throw new ArgumentOutOfRangeException(nameof(value), $"Card value '{value}' expected to be 2 character long.");

        var face = value[0] switch
        {
            'A' => Face.Aces,
            'K' => Face.Kings,
            'Q' => Face.Queens,
            'J' => Face.Jacks,
            'T' => Face.Tens,
            '9' => Face.Nines,
            '8' => Face.Eights,
            '7' => Face.Sevens,
            '6' => Face.Sixes,
            '5' => Face.Fives,
            '4' => Face.Fours,
            '3' => Face.Threes,
            '2' => Face.Twos,
            _ => throw new ArgumentException($"The first character '{value[0]}' is an unrecognized face value.", nameof(value))
        };

        var suit = value[1] switch
        {
            'C' => Suit.Clubs,
            'D' => Suit.Diamonds,
            'H' => Suit.Hearts,
            'S' => Suit.Spades,
            _ => throw new ArgumentException($"The second character '{value[1]} is an unrecognized suit value.", nameof(value))
        };

        Value = face & suit;
    }

    public static ulong operator |(ulong value, Card card) => value | card.Value;
    public static CardCollection operator |(Card card1, Card card2) => new(card1.Value | card2.Value);
    public static ulong operator &(ulong value, Card card) => value & card.Value;
    public static ulong operator ~(Card card) => ~card.Value;

    public override string ToString()
    {
        var str = new char[2];

        if ((Face.Twos & Value) != 0)
            str[0] = '2';
        else if ((Face.Threes & Value) != 0)
            str[0] = '3';
        else if ((Face.Fours & Value) != 0)
            str[0] = '4';
        else if ((Face.Fives & Value) != 0)
            str[0] = '5';
        else if ((Face.Sixes & Value) != 0)
            str[0] = '6';
        else if ((Face.Sevens & Value) != 0)
            str[0] = '7';
        else if ((Face.Eights & Value) != 0)
            str[0] = '8';
        else if ((Face.Nines & Value) != 0)
            str[0] = '9';
        else if ((Face.Tens & Value) != 0)
            str[0] = 'T';
        else if ((Face.Jacks & Value) != 0)
            str[0] = 'J';
        else if ((Face.Queens & Value) != 0)
            str[0] = 'Q';
        else if ((Face.Kings & Value) != 0)
            str[0] = 'K';
        else if ((Face.Aces & Value) != 0)
            str[0] = 'A';

        if ((Suit.Clubs & Value) != 0)
            str[1] = 'C';
        else if ((Suit.Diamonds & Value) != 0)
            str[1] = 'D';
        else if ((Suit.Hearts & Value) != 0)
            str[1] = 'H';
        else if ((Suit.Spades & Value) != 0)
            str[1] = 'S';
        
        return new string(str);
    }
}