namespace KallyPoker;

public struct Pot(PlayerCollection players)
{
    public PlayerBetCollection PreFlopBets = new(players);
    public PlayerBetCollection PostFlopBets = new(players);
    public PlayerBetCollection PostTurnBets = new(players);
    public PlayerBetCollection PostRiverBets = new(players);

    public void RunPreFlopBetting()
    {
        var maxLoop = 100;
        foreach (ref var pendingBet in PreFlopBets.EnumeratePendingBets(0))
        {
            if (maxLoop-- <= 0)
                throw new NotImplementedException();
            
#if DEBUG
            Console.WriteLine($"Handling bet from player {pendingBet.Player.Id}");
#endif
            pendingBet.State = PlayerBet.PlayerBetState.Checked;
        }
    }
}