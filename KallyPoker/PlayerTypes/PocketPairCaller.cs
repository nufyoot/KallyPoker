namespace KallyPoker.PlayerTypes;

public class PocketPairCaller : PlayerType
{
    protected override (BetType BetType, ulong? Value) InternalPlaceBet(CardCollection myHand, CardCollection tableCards, ulong minBet, ulong currentBet, ulong potSize, ulong myMoney)
    {
        if (myHand.FacesOnly().Count == 1)
            return (BetType.Call, null);

        return (BetType.Fold, null);
    }
}