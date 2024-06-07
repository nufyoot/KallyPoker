namespace KallyPoker.PlayerTypes;

public class Caller : PlayerType
{
    public override void PlaceBet(PlayerBetCollection currentBetCollection, ref PlayerBet playerBet)
    {
        if (playerBet.Bet.GetValueOrDefault() < currentBetCollection.MaxBet)
            Call(currentBetCollection, ref playerBet);
        else if (playerBet.Bet.GetValueOrDefault() == currentBetCollection.MaxBet)
            Check(currentBetCollection, ref playerBet);
    }
}