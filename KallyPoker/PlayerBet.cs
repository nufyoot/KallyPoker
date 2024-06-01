namespace KallyPoker;

public class PlayerBet(Player player, ulong? bet = null)
{
    public Player Player { get; } = player;
    public ulong? Bet { get; set; } = bet;

    public bool IsAllIn => Bet == Player.Money;
}