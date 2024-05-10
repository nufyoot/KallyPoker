namespace KallyPoker;

public readonly struct Suit
{
    public readonly ulong Mask;

    public static readonly Suit Clubs = new(0b1111111111111111UL);
    public static readonly Suit Diamonds = new(0b1111111111111111UL << 16);
    public static readonly Suit Hearts = new(0b1111111111111111UL << 32);
    public static readonly Suit Spades = new(0b1111111111111111UL << 48);

    private Suit(ulong mask) => Mask = mask;

    public static explicit operator ulong(Suit suit) => suit.Mask;

    public static ulong operator &(Suit suit, Face face) => suit.Mask & face.Mask;
    public static ulong operator &(Suit suit, ulong value) => suit.Mask & value;
}