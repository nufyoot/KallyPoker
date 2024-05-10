using System.Numerics;

namespace KallyPoker;

public struct Card
{
    private ulong _value;

    public Card()
    {
        _value = 0;
    }

    public Card(Suit suit, Face face)
    {
        _value = (ulong)suit & (ulong)face;
    }
}