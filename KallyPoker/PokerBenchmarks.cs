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
        const int maxLoop = 1;
#else
        const int maxLoop = 1_000_000;
#endif
        
        // Reset money
        for (var p = 0; p < 5; p++)
            table.Players[p] = new Player(p + 1, 100, new Caller(), CardCollection.Empty);
        
        // Setup the player types
        table.Players[4].PlayerType = new PocketPairCaller();

        for (var i = 0; i < maxLoop; i++)
        {
            _random.Shuffle(_cards);
            table.Reset();
            table.DealCards(_cards);

#if DEBUG
            Console.WriteLine($"Hand {i+1}");
            Console.WriteLine($"{table.Flop,-11}{table.Turn,-5}{table.River,-5}");

            var winners = HandChecker.GetWinningHands(table);
            for (var p = 0; p < winners.Length; p++)
                Console.WriteLine($"True winner: Player {winners.Players[p].Id}");

            Console.WriteLine("          Money                   Hand      Pre-flop       Flop           Turn           River          Best Hand");
            
            foreach(var player in table.Players)
            {
                var playerCards = CardCollection.Union(player.Cards, table.CardsAtRiver);
                var preFlopBestHand = HandChecker.GetBestHand(player.Cards);
                var flopBestHand = HandChecker.GetBestHand(CardCollection.Union(player.Cards, table.CardsAtFlop));
                var turnBestHand = HandChecker.GetBestHand(CardCollection.Union(player.Cards, table.CardsAtTurn));
                var riverBestHand = HandChecker.GetBestHand(playerCards);
                Console.WriteLine($"Player {player.Id}: (${player.Money,18:N})   {player.Cards,-10}{preFlopBestHand.Rank,-15}{flopBestHand.Rank,-15}{turnBestHand.Rank,-15}{riverBestHand.Rank,-15}{riverBestHand.Cards}");
            }
            Console.WriteLine();
#endif
        }
    }
}