using System.Runtime.CompilerServices;

namespace KallyPoker;

public ref struct PlayerBetCollection(PlayerBetArray playerBets)
{
    private PlayerBetArray _playerBets = playerBets;
    public int Count { get; private set; } 
    
    public PlayerBetCollection(PlayerCollection players)
        : this(new PlayerBetArray())
    {
        Count = 0;
        foreach(var player in players)
            _playerBets[Count++] = new PlayerBet(player);
    }

    public ulong TotalBet
    {
        get
        {
            var total = 0UL;
            foreach (var playerBet in _playerBets)
                total += playerBet.Bet.GetValueOrDefault();
            return total;
        }
    }
    
    public ulong MaxBet
    {
        get
        {
            var max = 0UL;
            foreach (var playerBet in _playerBets)
                max = Math.Max(max, playerBet.Bet.GetValueOrDefault());
            return max;
        }
    }
    
    public PlayerCollection ActivePlayers
    {
        get
        {
            var result = new PlayerArray();
            var count = 0;
            
            foreach (var playerBet in _playerBets)
                if (playerBet.Player.Money > 0 && playerBet.State != PlayerBet.PlayerBetState.Folded)
                    result[count++] = playerBet.Player;

            return new PlayerCollection(result, count);
        }
    }

    public Enumerator GetEnumerator() => new(ref _playerBets, 0);

    public ref struct Enumerator(ref PlayerBetArray playerBets, int startingSeat)
    {
        private readonly ref PlayerBetArray _playerBets = ref playerBets;
        private int _index = startingSeat - 1;

        public Enumerator GetEnumerator() => this;

        public ref PlayerBet Current => ref _playerBets[_index];

        public void Reset() => _index = startingSeat - 1;

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