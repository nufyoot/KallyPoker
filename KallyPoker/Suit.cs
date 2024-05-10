namespace KallyPoker;

public readonly struct Suit
{
    private readonly ulong _mask;

    public static readonly Suit Clubs = new(0b1111111111111111UL);
    public static readonly Suit Diamonds = new(0b1111111111111111UL << 16);
    public static readonly Suit Hearts = new(0b1111111111111111UL << 32);
    public static readonly Suit Spades = new(0b1111111111111111UL << 48);

    private Suit(ulong mask) => _mask = mask;

    public static explicit operator ulong(Suit suit) => suit._mask;
}