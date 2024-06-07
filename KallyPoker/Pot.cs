namespace KallyPoker;

public struct Pot
{
    private readonly int _dealerSeatNumber;

    public Pot(int dealerSeatNumber)
    {
        _dealerSeatNumber = dealerSeatNumber;
    }

    public PlayerBetCollection RunPreFlopBetting(PlayerCollection activePlayers)
    {
        Log.Info(ConsoleColor.Cyan, $"Beginning pre-flop betting");

        var preFlopBets = new PlayerBetCollection(activePlayers);
        var pendingBetEnumerator = preFlopBets.EnumeratePendingBets(_dealerSeatNumber);
        
        // There's some logic to build out here around having the dealer start betting with SB and BB 
        // putting money in the pot. For now, small blind will be the same as the dealer and we can fix this
        // at a later date.

        pendingBetEnumerator.MoveNext();
        pendingBetEnumerator.Current.Bet = 1;
        Log.Info($"Player {pendingBetEnumerator.Current.Player.Id} (SB) put in $1");
        pendingBetEnumerator.MoveNext();
        pendingBetEnumerator.Current.Bet = 2;
        Log.Info($"Player {pendingBetEnumerator.Current.Player.Id} (BB) put in $2");
        
        var maxLoop = 100;
        foreach (ref var pendingBet in pendingBetEnumerator)
        {
            if (maxLoop-- <= 0)
                throw new NotImplementedException();
            
            Log.Info($"Handling bet from player {pendingBet.Player.Id}");
            
            // Perform the bet
            pendingBet.Player.PlayerType.PlaceBet(preFlopBets, ref pendingBet);
        }
        
        // Subtract money from players
        foreach (ref var playerBet in preFlopBets)
            playerBet.Player.Money -= playerBet.Bet.GetValueOrDefault();

        return preFlopBets;
    }
    
    public PlayerBetCollection RunFlopBetting(PlayerCollection activePlayers)
    {
        Log.Info(ConsoleColor.Cyan, $"Beginning flop betting");

        var flopBets = new PlayerBetCollection(activePlayers);
        var pendingBetEnumerator = flopBets.EnumeratePendingBets(_dealerSeatNumber);
        
        // Skip the dealer as they action will end with them.
        pendingBetEnumerator.MoveNext();
        
        var maxLoop = 100;
        foreach (ref var pendingBet in pendingBetEnumerator)
        {
            if (maxLoop-- <= 0)
                throw new NotImplementedException();
            
            Log.Info($"Handling bet from player {pendingBet.Player.Id}");
            
            // Perform the bet
            pendingBet.Player.PlayerType.PlaceBet(flopBets, ref pendingBet);
        }

        return flopBets;
    }
    
    public PlayerBetCollection RunTurnBetting(PlayerCollection activePlayers)
    {
        Log.Info(ConsoleColor.Cyan, $"Beginning turn betting");

        var turnBets = new PlayerBetCollection(activePlayers);
        var pendingBetEnumerator = turnBets.EnumeratePendingBets(_dealerSeatNumber);
        
        // Skip the dealer as they action will end with them.
        pendingBetEnumerator.MoveNext();
        
        var maxLoop = 100;
        foreach (ref var pendingBet in pendingBetEnumerator)
        {
            if (maxLoop-- <= 0)
                throw new NotImplementedException();
            
            Log.Info($"Handling bet from player {pendingBet.Player.Id}");
            
            // Perform the bet
            pendingBet.Player.PlayerType.PlaceBet(turnBets, ref pendingBet);
        }

        return turnBets;
    }
    
    public PlayerBetCollection RunRiverBetting(PlayerCollection activePlayers)
    {
        Log.Info(ConsoleColor.Cyan,$"Beginning river betting");

        var riverBets = new PlayerBetCollection(activePlayers);
        var pendingBetEnumerator = riverBets.EnumeratePendingBets(_dealerSeatNumber);
        
        // Skip the dealer as they action will end with them.
        pendingBetEnumerator.MoveNext();
        
        var maxLoop = 100;
        foreach (ref var pendingBet in pendingBetEnumerator)
        {
            if (maxLoop-- <= 0)
                throw new NotImplementedException();
            
            Log.Info($"Handling bet from player {pendingBet.Player.Id}");
            
            // Perform the bet
            pendingBet.Player.PlayerType.PlaceBet(riverBets, ref pendingBet);
        }

        return riverBets;
    }
}