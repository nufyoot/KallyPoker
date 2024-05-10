namespace KallyPoker;

public readonly struct Face
{
    private const ulong BaseMask = 0b0000000000000001000000000000000100000000000000010000000000000001;
    private readonly ulong _mask;

    public static readonly Face Twos = new(BaseMask);
    public static readonly Face Threes = new(BaseMask << 1);
    public static readonly Face Fours = new(BaseMask << 2);
    public static readonly Face Fives = new(BaseMask << 3);
    public static readonly Face Sixes = new(BaseMask << 4);
    public static readonly Face Sevens = new(BaseMask << 5);
    public static readonly Face Eights = new(BaseMask << 6);
    public static readonly Face Nines = new(BaseMask << 7);
    public static readonly Face Tens = new(BaseMask << 8);
    public static readonly Face Jacks = new(BaseMask << 9);
    public static readonly Face Queens = new(BaseMask << 10);
    public static readonly Face Kings = new(BaseMask << 11);
    public static readonly Face Aces = new(BaseMask << 12);

    private Face(ulong mask) => _mask = mask;

    public static explicit operator ulong(Face face) => face._mask;
}