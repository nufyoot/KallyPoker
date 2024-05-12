namespace KallyPoker;

public readonly struct Suit(ulong mask)
{
    public readonly ulong Mask = mask;

    public const ulong ClubsMask = 0b0001111111111111UL;
    public const ulong DiamondsMask = 0b0001111111111111UL << 16;
    public const ulong HeartsMask = 0b0001111111111111UL << 32;
    public const ulong SpadesMask = 0b0001111111111111UL << 48;

    public static readonly Suit Clubs = new(ClubsMask);
    public static readonly Suit Diamonds = new(DiamondsMask);
    public static readonly Suit Hearts = new(HeartsMask);
    public static readonly Suit Spades = new(SpadesMask);

    public static explicit operator ulong(Suit suit) => suit.Mask;

    public static ulong operator &(Suit suit, Face face) => suit.Mask & face.Mask;
    public static ulong operator &(Suit suit, ulong value) => suit.Mask & value;
}