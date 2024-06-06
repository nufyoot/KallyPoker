using System.Runtime.CompilerServices;

namespace KallyPoker;

[InlineArray(PokerConstants.PlayerCount)]
public struct PlayerBetCollection
{
    private PlayerBet _element0;

    public PlayerBetCollection(PlayerCollection players)
    {
        for (var i = 0; i < PokerConstants.PlayerCount; i++)
            this[i] = new PlayerBet(players[i]);
    }

    public ulong TotalBet
    {
        get
        {
            var total = 0UL;
            foreach (var playerBet in this)
                total += playerBet.Bet.GetValueOrDefault();
            return total;
        }
    }

    public ref struct PendingPlayerBetEnumerator(ref PlayerBetCollection playerBets, int startingSeat)
    {
        private readonly ref PlayerBetCollection _playerBets = ref playerBets;
        private int _index = startingSeat - 1;

        public PendingPlayerBetEnumerator GetEnumerator() => this;

        public ref PlayerBet Current => ref _playerBets[_index];

        public bool MoveNext()
        {
            int newIndex;
            
            for (newIndex = (_index + 1) % PokerConstants.PlayerCount; newIndex != _index; newIndex = (newIndex + 1) % PokerConstants.PlayerCount)
            {
                var playerBet = _playerBets[newIndex];
                
                // Skip anyone who has gone all-in
                if (playerBet.IsAllIn)
                    continue;
                
                // Skip anyone who has folded
                if (playerBet.State == PlayerBet.PlayerBetState.Folded)
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
                if (playerBet.Bet.GetValueOrDefault() == MaxBet)
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
        
        private ulong MaxBet
        {
            get
            {
                var max = 0UL;
                foreach (var playerBet in _playerBets)
                    max = Math.Max(max, playerBet.Bet.GetValueOrDefault());
                return max;
            }
        }
    }
}

public static class PlayerBetCollectionExtensions
{
    public static PlayerBetCollection.PendingPlayerBetEnumerator EnumeratePendingBets(ref this PlayerBetCollection playerBets, int startingSeat) => 
        new(ref playerBets, startingSeat);
}