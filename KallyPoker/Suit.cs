namespace KallyPoker;

public readonly struct Suit(ulong mask)
{
    public readonly ulong Mask = mask;

    public const int ClubsBitShift = 48;
    public const int DiamondsBitShift = 32;
    public const int HeartsBitShift = 16;
    public const int SpadesBitShift = 0;

    public const ulong ClubsMask = 0b0001111111111111UL << ClubsBitShift;
    public const ulong DiamondsMask = 0b0001111111111111UL << DiamondsBitShift;
    public const ulong HeartsMask = 0b0001111111111111UL << HeartsBitShift;
    public const ulong SpadesMask = 0b0001111111111111UL << SpadesBitShift;

    public static readonly Suit Clubs = new(ClubsMask);
    public static readonly Suit Diamonds = new(DiamondsMask);
    public static readonly Suit Hearts = new(HeartsMask);
    public static readonly Suit Spades = new(SpadesMask);
}