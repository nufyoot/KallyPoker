namespace KallyPoker;

public readonly struct Suit
{
    private readonly ulong _mask;

    public static readonly Suit Clubs = new(65535UL);
    public static readonly Suit Diamonds = new(65535UL << 16);
    public static readonly Suit Hearts = new(65535UL << 32);
    public static readonly Suit Spades = new(65535UL << 48);

    private Suit(ulong mask) => _mask = mask;

    public static explicit operator ulong(Suit suit) => suit._mask;
}