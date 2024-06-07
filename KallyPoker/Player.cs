using KallyPoker.PlayerTypes;

namespace KallyPoker;

public class Player(int id, ulong money, PlayerType playerType, CardCollection cards)
{
    public CardCollection Cards { get; set; } = cards;
    public ulong Money { get; set; } = money;
    public int Id { get; } = id;
    public PlayerType PlayerType { get; set; } = playerType;
}