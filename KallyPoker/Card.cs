namespace KallyPoker;

public readonly struct Card(ulong bits)
{
    private readonly ulong _bits = bits;

    public Card(Suit suit, Face face) : this(suit.Mask & face.Mask)
    {
    }

    public static ulong operator |(ulong inputValue, Card card) => inputValue | card._bits;

    public static CardCollection operator +(Card card1, Card card2) => new(card1._bits | card2._bits);

    public static implicit operator ulong(Card card) => card._bits;

    public int CompareTo(Card other)
    {
        var thisFace =
            ((_bits & Suit.ClubsMask) >> Suit.ClubsBitShift) |
            ((_bits & Suit.DiamondsMask) >> Suit.DiamondsBitShift) |
            ((_bits & Suit.HeartsMask) >> Suit.HeartsBitShift) |
            ((_bits & Suit.SpadesMask) >> Suit.SpadesBitShift);
        var otherFace = 
            ((other._bits & Suit.ClubsMask) >> Suit.ClubsBitShift) |
            ((other._bits & Suit.DiamondsMask) >> Suit.DiamondsBitShift) |
            ((other._bits & Suit.HeartsMask) >> Suit.HeartsBitShift) |
            ((other._bits & Suit.SpadesMask) >> Suit.SpadesBitShift);

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

        if ((Face.Twos.Mask & _bits) != 0)
            str[0] = '2';
        else if ((Face.Threes.Mask & _bits) != 0)
            str[0] = '3';
        else if ((Face.Fours.Mask & _bits) != 0)
            str[0] = '4';
        else if ((Face.Fives.Mask & _bits) != 0)
            str[0] = '5';
        else if ((Face.Sixes.Mask & _bits) != 0)
            str[0] = '6';
        else if ((Face.Sevens.Mask & _bits) != 0)
            str[0] = '7';
        else if ((Face.Eights.Mask & _bits) != 0)
            str[0] = '8';
        else if ((Face.Nines.Mask & _bits) != 0)
            str[0] = '9';
        else if ((Face.Tens.Mask & _bits) != 0)
            str[0] = 'T';
        else if ((Face.Jacks.Mask & _bits) != 0)
            str[0] = 'J';
        else if ((Face.Queens.Mask & _bits) != 0)
            str[0] = 'Q';
        else if ((Face.Kings.Mask & _bits) != 0)
            str[0] = 'K';
        else if ((Face.Aces.Mask & _bits) != 0)
            str[0] = 'A';

        if ((Suit.Clubs.Mask & _bits) != 0)
            str[1] = 'C';
        else if ((Suit.Diamonds.Mask & _bits) != 0)
            str[1] = 'D';
        else if ((Suit.Hearts.Mask & _bits) != 0)
            str[1] = 'H';
        else if ((Suit.Spades.Mask & _bits) != 0)
            str[1] = 'S';
        
        return new string(str);
    }
}