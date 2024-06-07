namespace KallyPoker.PlayerTypes;

public class PocketPairCaller : PlayerType
{
    public override void PlaceBet(PlayerBetCollection currentBetCollection, ref PlayerBet playerBet)
    {
        if (playerBet.Player.Cards.FacesOnly().Count != 1)
            Fold(currentBetCollection, ref playerBet);
        else if (playerBet.Bet.GetValueOrDefault() < currentBetCollection.MaxBet)
            Call(currentBetCollection, ref playerBet);
        else if (playerBet.Bet.GetValueOrDefault() == currentBetCollection.MaxBet)
            Check(currentBetCollection, ref playerBet);
    }
}