using System.Runtime.CompilerServices;

namespace KallyPoker;

[InlineArray(5)]
public struct PlayerBetCollection
{
    private PlayerBet _element0;

    public PlayerBetCollection(PlayerCollection players)
    {
        this[0] = new PlayerBet(players[0]);
        this[1] = new PlayerBet(players[1]);
        this[2] = new PlayerBet(players[2]);
        this[3] = new PlayerBet(players[3]);
        this[4] = new PlayerBet(players[4]);
    }

    public ulong MaxBet
    {
        get
        {
            var max = 0UL;
            foreach (var playerBet in this)
                max = Math.Max(max, playerBet.Bet.GetValueOrDefault());
            return max;
        }
    }

    public ulong TotalBet =>
        this[0].Bet.GetValueOrDefault() +
        this[1].Bet.GetValueOrDefault() +
        this[2].Bet.GetValueOrDefault() +
        this[3].Bet.GetValueOrDefault() +
        this[4].Bet.GetValueOrDefault();

    private ref struct PendingPlayerBetEnumerator
    {
        
    }
}