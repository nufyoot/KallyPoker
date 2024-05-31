namespace KallyPoker.PlayerTypes;

public class Caller : PlayerType
{
    protected override (BetType BetType, ulong? Value) InternalPlaceBet(CardCollection myHand, CardCollection tableCards, ulong minBet, ulong currentBet, ulong potSize, ulong myMoney)
    {
        return (BetType.Call, null);
    }
}