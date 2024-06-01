namespace KallyPoker;

public struct Pot(PlayerCollection players)
{
    public PlayerBetCollection PreFlopBets = new(players);
    public PlayerBetCollection PostFlopBets = new(players);
    public PlayerBetCollection PostTurnBets = new(players);
    public PlayerBetCollection PostRiverBets = new(players);
}