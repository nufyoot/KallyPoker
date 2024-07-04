using System.Numerics;
using KallyPoker;
using KallyPoker.PlayerTypes.HeadsUpHoldem;

var random = new Random(42);

var deck = CardCollection.FullDeck.ToArray();
var totalBet = 0m;
var totalWin = 0m;
var player = new LearningPlayer();
var print = false;

var vector = Vector<int>.Count;

for (var i = 0; i < 10000; i++)
{
    random.Shuffle(deck);

    var playerCards = new CardCollection(deck[0] | deck[2]);
    var dealerCards = new CardCollection(deck[1] | deck[3]);
    var communityCards = new CardCollection(deck[4] | deck[5] | deck[6] | deck[7] | deck[8]);
    var postFlop = CardCollection.Union(playerCards, new CardCollection(deck[4] | deck[5] | deck[6]));

    var headsUpResult = HeadsUpHoldem.CheckHand(communityCards, dealerCards, playerCards);
    var bet = player.DecideBet(new Hand(deck[4], deck[5], deck[6], deck[7], deck[8]), playerCards);
    var winnings = HeadsUpHoldem.CalculateWin(headsUpResult, bet);

    if (print)
    {
        Console.WriteLine($"Hand {i + 1}");
        Console.WriteLine($"  Community: {communityCards}");
        Console.WriteLine($"  Dealer:    {dealerCards} ({headsUpResult.DealerHandResult.Rank})");
        Console.WriteLine($"  Player:    {playerCards} ({headsUpResult.PlayerHandResult.Rank})");
        Console.WriteLine($"  Win:       ${headsUpResult.PlayerWins}");
        Console.WriteLine($"  Bet:       ${bet.ToString()}");
        Console.WriteLine($"  Winnings:  ${winnings}");
        Console.WriteLine();
    }
    
    totalBet += bet.Total;
    totalWin += winnings;
}

Console.WriteLine($"Summary: ${totalWin} / ${totalBet} ({Math.Round(totalWin * 100 / totalBet, 0)}%)");