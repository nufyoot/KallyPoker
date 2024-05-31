namespace KallyPoker.PlayerTypes;

public abstract class PlayerType
{
    public (BetType BetType, ulong? Value) PlaceBet(CardCollection myHand, CardCollection tableCards, ulong minBet, ulong currentBet, ulong potSize, ulong myMoney)
    {
        var desiredBet = InternalPlaceBet(myHand, tableCards, minBet, currentBet, potSize, myMoney);

        if (desiredBet.BetType == BetType.Fold)
            return (BetType.Fold, null);

        if (desiredBet.BetType == BetType.Call)
            return (BetType.Call, Math.Min(myMoney, currentBet));

        if (desiredBet.BetType == BetType.Check)
        {
            
        }

        return desiredBet;
    }

    protected abstract (BetType BetType, ulong? Value) InternalPlaceBet(CardCollection myHand, CardCollection tableCards, ulong minBet, ulong currentBet, ulong potSize, ulong myMoney);
}