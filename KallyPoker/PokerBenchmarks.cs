using BenchmarkDotNet.Attributes;

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
        var players = new PlayerCollection();
        var table = new Table();
        
#if DEBUG
        const int maxLoop = 100;
#else
        const int maxLoop = 1_000_000;
#endif
        
        for (var i = 0; i < maxLoop; i++)
        {
            players[0].ResetCards();
            players[1].ResetCards();
            players[2].ResetCards();
            players[3].ResetCards();
            players[4].ResetCards();
            table.Reset();
    
            _random.Shuffle(_cards);
    
            // Deal the first card.
            players[0].AddCard(_cards[0]);
            players[1].AddCard(_cards[1]);
            players[2].AddCard(_cards[2]);
            players[3].AddCard(_cards[3]);
            players[4].AddCard(_cards[4]);
    
            // Deal the second card.
            players[0].AddCard(_cards[5]);
            players[1].AddCard(_cards[6]);
            players[2].AddCard(_cards[7]);
            players[3].AddCard(_cards[8]);
            players[4].AddCard(_cards[9]);
    
            // Deal the flop
            table.Flop = new CardCollection(_cards[11].Value | _cards[12].Value | _cards[13].Value);
    
            // Deal the turn
            table.Turn = _cards[15];
    
            // Deal the river
            table.River = _cards[17];

#if DEBUG
            for (var p = 0; p < 5; p++)
            {
                var playerCards = players[p].Cards;
                playerCards.Add(table.Cards);
                var preFlopBestHand = HandChecker.GetBestHand(players[p].Cards);
                var flopBestHand = HandChecker.GetBestHand(new CardCollection(players[p].Cards.Value | table.Flop.Value));
                var turnBestHand = HandChecker.GetBestHand(new CardCollection(players[p].Cards.Value | table.Flop.Value | table.Turn.Value));
                var bestHand = HandChecker.GetBestHand(playerCards);
                Console.WriteLine($"{table.Flop,-10}{table.Turn,-5}{table.River,-5}{table.Cards,-20}{players[p].Cards,-10}{playerCards,-25}{preFlopBestHand.Rank,-15}{flopBestHand.Rank,-15}{turnBestHand.Rank,-15}{bestHand.Rank,-15}{bestHand.Cards}");
            }
            Console.WriteLine();
#endif
        }
    }
}