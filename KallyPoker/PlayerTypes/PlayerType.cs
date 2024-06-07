using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KallyPoker.PlayerTypes;

public abstract class PlayerType
{
    public abstract void PlaceBet(PlayerBetCollection currentBetCollection, ref PlayerBet playerBet);

    protected static void Check(PlayerBetCollection currentBetCollection, ref PlayerBet playerBet)
    {
        playerBet.State = PlayerBet.PlayerBetState.Checked;
        Log.Info($"Player {playerBet.Player.Id} ({playerBet.Player.PlayerType.GetType().Name}) checked");
    }

    protected static void Call(PlayerBetCollection currentBetCollection, ref PlayerBet playerBet)
    {
        playerBet.Bet = Math.Min(currentBetCollection.MaxBet, playerBet.Player.Money);
        playerBet.State = PlayerBet.PlayerBetState.Called;
        Log.Info($"Player {playerBet.Player.Id} ({playerBet.Player.PlayerType.GetType().Name}) called for ${playerBet.Bet}");
    }

    protected static void Fold(PlayerBetCollection currentBetCollection, ref PlayerBet playerBet)
    {
        playerBet.State = PlayerBet.PlayerBetState.Folded;
        Log.Info($"Player {playerBet.Player.Id} ({playerBet.Player.PlayerType.GetType().Name}) folded");
    }
}