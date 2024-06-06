namespace KallyPoker;

public struct PlayerBet(Player player, ulong? bet = null)
{
    public enum PlayerBetState
    {
        NotPlayed,
        Checked,
        Called,
        Raised,
        Folded,
    }
    
    public Player Player { get; } = player;
    public ulong? Bet { get; set; } = bet;
    public PlayerBetState State { get; set; } = PlayerBetState.NotPlayed;

    public bool IsAllIn => Bet.HasValue && Bet.GetValueOrDefault() == Player.Money;
}