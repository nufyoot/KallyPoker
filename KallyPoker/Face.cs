namespace KallyPoker;

public readonly struct Face(ulong mask)
{
    private const ulong BaseMask = 0b0000000000000001000000000000000100000000000000010000000000000001UL;
    public const ulong TwosMask = BaseMask;
    public const ulong ThreesMask = BaseMask << 1;
    public const ulong FoursMask = BaseMask << 2;
    public const ulong FivesMask = BaseMask << 3;
    public const ulong SixesMask = BaseMask << 4;
    public const ulong SevensMask = BaseMask << 5;
    public const ulong EightsMask = BaseMask << 6;
    public const ulong NinesMask = BaseMask << 7;
    public const ulong TensMask = BaseMask << 8;
    public const ulong JacksMask = BaseMask << 9;
    public const ulong QueensMask = BaseMask << 10;
    public const ulong KingsMask = BaseMask << 11;
    public const ulong AcesMask = BaseMask << 12;
    
    public readonly ulong Mask = mask;

    public static readonly Face Twos = new(TwosMask);
    public static readonly Face Threes = new(ThreesMask);
    public static readonly Face Fours = new(FoursMask);
    public static readonly Face Fives = new(FivesMask);
    public static readonly Face Sixes = new(SixesMask);
    public static readonly Face Sevens = new(SevensMask);
    public static readonly Face Eights = new(EightsMask);
    public static readonly Face Nines = new(NinesMask);
    public static readonly Face Tens = new(TensMask);
    public static readonly Face Jacks = new(JacksMask);
    public static readonly Face Queens = new(QueensMask);
    public static readonly Face Kings = new(KingsMask);
    public static readonly Face Aces = new(AcesMask);
}