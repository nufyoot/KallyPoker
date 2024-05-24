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
        Value = suit.Mask & face.Mask;
    }

    public static ErrorTuple<Card> Parse(ReadOnlySpan<char> value)
    {
        if (value.Length != 2)
            return new Error($"Card value '{value}' expected to be 2 character long.");

        ErrorTuple<Face> face = value[0] switch
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
            _ => new Error($"The first character '{value[0]}' is an unrecognized face value.")
        };
        
        if (face.HasError)
            return face.Error;

        ErrorTuple<Suit> suit = value[1] switch
        {
            'C' => Suit.Clubs,
            'D' => Suit.Diamonds,
            'H' => Suit.Hearts,
            'S' => Suit.Spades,
            _ => new Error($"The second character '{value[1]} is an unrecognized suit value.")
        };

        if (suit.HasError)
            return suit.Error;
        
        return new Card(suit.Result, face.Result);
    }

    public override string ToString()
    {
        var str = new char[2];

        if ((Face.Twos.Mask & Value) != 0)
            str[0] = '2';
        else if ((Face.Threes.Mask & Value) != 0)
            str[0] = '3';
        else if ((Face.Fours.Mask & Value) != 0)
            str[0] = '4';
        else if ((Face.Fives.Mask & Value) != 0)
            str[0] = '5';
        else if ((Face.Sixes.Mask & Value) != 0)
            str[0] = '6';
        else if ((Face.Sevens.Mask & Value) != 0)
            str[0] = '7';
        else if ((Face.Eights.Mask & Value) != 0)
            str[0] = '8';
        else if ((Face.Nines.Mask & Value) != 0)
            str[0] = '9';
        else if ((Face.Tens.Mask & Value) != 0)
            str[0] = 'T';
        else if ((Face.Jacks.Mask & Value) != 0)
            str[0] = 'J';
        else if ((Face.Queens.Mask & Value) != 0)
            str[0] = 'Q';
        else if ((Face.Kings.Mask & Value) != 0)
            str[0] = 'K';
        else if ((Face.Aces.Mask & Value) != 0)
            str[0] = 'A';

        if ((Suit.Clubs.Mask & Value) != 0)
            str[1] = 'C';
        else if ((Suit.Diamonds.Mask & Value) != 0)
            str[1] = 'D';
        else if ((Suit.Hearts.Mask & Value) != 0)
            str[1] = 'H';
        else if ((Suit.Spades.Mask & Value) != 0)
            str[1] = 'S';
        
        return new string(str);
    }
}