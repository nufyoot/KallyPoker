using BenchmarkDotNet.Attributes;
using KallyPoker.PlayerTypes;

namespace KallyPoker;

[MemoryDiagnoser]
public class PokerBenchmarks
{
    private static readonly Card[] _cards;
    private static readonly Random _random;
    
    static PokerBenchmarks()
    {
        var cardCollection = CardCollection.FullDeck;
        _cards = cardCollection.ToArray();
        _random = new Random(42);
    }

    private class Score
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
    }

    [Benchmark]
    public void RunTest()
    {
        var table = new Table();
        
#if DEBUG
        const int maxLoop = 150;
#else
        const int maxLoop = 1_000_000;
#endif
        
        // Reset money
        for (var p = 0; p < 5; p++)
        {
            table.Players[p].Money = 1_000_000_000;
            table.Players[p].Id = (p + 1);
        }
        
        // Setup the player types
        table.Players[0].PlayerType = new Caller();
        table.Players[1].PlayerType = new Caller();
        table.Players[2].PlayerType = new Caller();
        table.Players[3].PlayerType = new Caller();
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