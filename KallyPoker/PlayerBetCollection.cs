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

    private ref struct PendingPlayerBetEnumerator(PlayerBetCollection playerBets)
    {
        private readonly PlayerBetCollection _playerBets = playerBets;
        private int _index;
        
        
        
        public bool MoveNext()
        {
            int newIndex;
            
            for (newIndex = (_index + 1) % 5; newIndex != _index; newIndex = (newIndex + 1) % 5)
            {
                var playerBet = _playerBets[newIndex];
                
                // Skip anyone who has gone all-in
                if (playerBet.IsAllIn)
                    continue;
                
                // Skip anyone who has no more money.
                if (playerBet.Player.Money == 0)
                    continue;

                // If we haven't played yet, allow it to happen now.
                if (playerBet.State == PlayerBet.PlayerBetState.NotPlayed)
                    break;
                
                // Ok, at this point we've played at least once, we haven't gone all-in, and we have money left.
                // Determine if we're already matching the max bet. If we are already matching the max bet, 
                // continue to the next player.
                if (playerBet.Bet.GetValueOrDefault() == _playerBets.MaxBet)
                    continue;
                
                // At this point, the player hasn't folded, they didn't go all-in, they have money, and their bet
                // doesn't match the max bet put forth. This seems like a good place to stop.
                break;
            }

            // If we made it back around to the current index, there are no more players to look at.
            if (newIndex == _index)
                return false;
            
            _index = newIndex;
            return true;
        }
    }
}