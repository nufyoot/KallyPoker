using System.Numerics;

namespace KallyPoker;

public readonly struct Card
{
    private readonly ulong _value;

    public Card()
    {
        _value = 0;
    }

    public Card(Suit suit, Face face)
    {
        _value = suit & face;
    }

    public override string ToString()
    {
        var str = new char[2];

        if ((Face.Twos & _value) != 0)
            str[0] = '2';
        else if ((Face.Threes & _value) != 0)
            str[0] = '3';
        else if ((Face.Fours & _value) != 0)
            str[0] = '4';
        else if ((Face.Fives & _value) != 0)
            str[0] = '5';
        else if ((Face.Sixes & _value) != 0)
            str[0] = '6';
        else if ((Face.Sevens & _value) != 0)
            str[0] = '7';
        else if ((Face.Eights & _value) != 0)
            str[0] = '8';
        else if ((Face.Nines & _value) != 0)
            str[0] = '9';
        else if ((Face.Tens & _value) != 0)
            str[0] = 'T';
        else if ((Face.Jacks & _value) != 0)
            str[0] = 'J';
        else if ((Face.Queens & _value) != 0)
            str[0] = 'Q';
        else if ((Face.Kings & _value) != 0)
            str[0] = 'K';
        else if ((Face.Aces & _value) != 0)
            str[0] = 'A';

        if ((Suit.Clubs & _value) != 0)
            str[1] = 'C';
        else if ((Suit.Diamonds & _value) != 0)
            str[1] = 'D';
        else if ((Suit.Hearts & _value) != 0)
            str[1] = 'H';
        else if ((Suit.Spades & _value) != 0)
            str[1] = 'S';
        
        return new string(str);
    }
}