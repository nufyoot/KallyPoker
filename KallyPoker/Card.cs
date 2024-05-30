namespace KallyPoker;

public readonly struct Card(ulong bits)
{
    public readonly ulong Bits = bits;

    public Card(Suit suit, Face face) : this(suit.Mask & face.Mask)
    {
    }

    public int CompareTo(Card other)
    {
        var thisFace =
            ((Bits & Suit.ClubsMask) >> Suit.ClubsBitShift) |
            ((Bits & Suit.DiamondsMask) >> Suit.DiamondsBitShift) |
            ((Bits & Suit.HeartsMask) >> Suit.HeartsBitShift) |
            ((Bits & Suit.SpadesMask) >> Suit.SpadesBitShift);
        var otherFace = 
            ((other.Bits & Suit.ClubsMask) >> Suit.ClubsBitShift) |
            ((other.Bits & Suit.DiamondsMask) >> Suit.DiamondsBitShift) |
            ((other.Bits & Suit.HeartsMask) >> Suit.HeartsBitShift) |
            ((other.Bits & Suit.SpadesMask) >> Suit.SpadesBitShift);

        return thisFace.CompareTo(otherFace);
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

        if ((Face.Twos.Mask & Bits) != 0)
            str[0] = '2';
        else if ((Face.Threes.Mask & Bits) != 0)
            str[0] = '3';
        else if ((Face.Fours.Mask & Bits) != 0)
            str[0] = '4';
        else if ((Face.Fives.Mask & Bits) != 0)
            str[0] = '5';
        else if ((Face.Sixes.Mask & Bits) != 0)
            str[0] = '6';
        else if ((Face.Sevens.Mask & Bits) != 0)
            str[0] = '7';
        else if ((Face.Eights.Mask & Bits) != 0)
            str[0] = '8';
        else if ((Face.Nines.Mask & Bits) != 0)
            str[0] = '9';
        else if ((Face.Tens.Mask & Bits) != 0)
            str[0] = 'T';
        else if ((Face.Jacks.Mask & Bits) != 0)
            str[0] = 'J';
        else if ((Face.Queens.Mask & Bits) != 0)
            str[0] = 'Q';
        else if ((Face.Kings.Mask & Bits) != 0)
            str[0] = 'K';
        else if ((Face.Aces.Mask & Bits) != 0)
            str[0] = 'A';

        if ((Suit.Clubs.Mask & Bits) != 0)
            str[1] = 'C';
        else if ((Suit.Diamonds.Mask & Bits) != 0)
            str[1] = 'D';
        else if ((Suit.Hearts.Mask & Bits) != 0)
            str[1] = 'H';
        else if ((Suit.Spades.Mask & Bits) != 0)
            str[1] = 'S';
        
        return new string(str);
    }
}