using BenchmarkDotNet.Attributes;
using KallyPoker.PlayerTypes;

namespace KallyPoker;

[MemoryDiagnoser]
public class PokerBenchmarks
{
    private readonly Card[] _cards;
    private readonly Random _random;
    
    public PokerBenchmarks()
    {
        var cardCollection = CardCollection.FullDeck;
        _cards = cardCollection.ToArray();
        _random = new Random(42);
    }

    [Benchmark]
    public void RunTest()
    {
        var table = new Table();
        
#if DEBUG
        const int maxLoop = 10;
#else
        const int maxLoop = 1_000_000;
#endif
        
        // Reset money
        for (var p = 0; p < PokerConstants.PlayerCount; p++)
            table.Players[p] = new Player(p + 1, 100, new Caller(), CardCollection.Empty);
        
        // Set up the player types
        table.Players[4].PlayerType = new PocketPairCaller();

        for (var i = 0; i < maxLoop; i++)
        {
            _random.Shuffle(_cards);
            table.Reset();
            table.DealCards(_cards);
            
            Log.Info($"Hand {i+1}, Dealer seat: {table.DealerSeatNumber} which is Player {table.ActivePlayers[table.DealerSeatNumber].Id}");
            Log.Info($"{table.Flop,-11}{table.Turn,-5}{table.River,-5}");

            var pot = new Pot(table.DealerSeatNumber);
            var trueWinners = HandChecker.GetWinningHands(table.Players, table.CardsAtRiver);
            
            var preFlopBets = pot.RunPreFlopBetting(table.ActivePlayers);
            var flopBets = pot.RunFlopBetting(preFlopBets.ActivePlayers);
            var turnBets = pot.RunTurnBetting(flopBets.ActivePlayers);
            var riverBets = pot.RunRiverBetting(turnBets.ActivePlayers);

            var winners = HandChecker.GetWinningHands(riverBets.ActivePlayers, table.CardsAtRiver);
#if DEBUG

            for (var p = 0; p < trueWinners.Length; p++)
                Log.Info($"True winner: Player {trueWinners.Players[p].Id}");
            
            for (var p = 0; p < winners.Length; p++)
                Log.Info($"Actual winner: Player {trueWinners.Players[p].Id}");

            Log.Info("          Money                   Hand      Pre-flop       Flop           Turn           River          Best Hand");
            
            foreach(var player in table.ActivePlayers)
            {
                var playerCards = CardCollection.Union(player.Cards, table.CardsAtRiver);
                var preFlopBestHand = HandChecker.GetBestHand(player.Cards);
                var flopBestHand = HandChecker.GetBestHand(CardCollection.Union(player.Cards, table.CardsAtFlop));
                var turnBestHand = HandChecker.GetBestHand(CardCollection.Union(player.Cards, table.CardsAtTurn));
                var riverBestHand = HandChecker.GetBestHand(playerCards);
                Log.Info($"Player {player.Id}: (${player.Money,18:N})   {player.Cards,-10}{preFlopBestHand.Rank,-15}{flopBestHand.Rank,-15}{turnBestHand.Rank,-15}{riverBestHand.Rank,-15}{riverBestHand.Cards}");
            }
#endif
            
            // Determine if there are enough players to keep playing
            if (table.ActivePlayers.Count == 1)
            {
                Log.Info("Only 1 player remaining, stopping the game.");
                break;
            }
            
            // Figure out the next dealer button location
            for (var p = (table.DealerSeatNumber + 1) % PokerConstants.PlayerCount; p != table.DealerSeatNumber; p++)
            {
                var player = table.Players[p];
                if (player.Money == 0)
                    continue;
                
                table.DealerSeatNumber = p;
                break;
            }
        }
    }
}