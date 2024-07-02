using KallyPoker;

var random = new Random(42);

var deck = CardCollection.FullDeck.ToArray();
var totalBet = 0m;
var totalWin = 0m;

for (var i = 0; i < 100; i++)
{
    random.Shuffle(deck);

    var playerCards = new CardCollection(deck[0] | deck[2]);
    var dealerCards = new CardCollection(deck[1] | deck[3]);
    var communityCards = new CardCollection(deck[4] | deck[5] | deck[6] | deck[7] | deck[8]);

    var headsUpResult = HeadsUpHoldem.CheckHand(communityCards, dealerCards, playerCards);

    Console.WriteLine($"Hand {i}");
    Console.WriteLine($"  Community: {communityCards}");
    Console.WriteLine($"  Dealer:    {dealerCards}");
    Console.WriteLine($"  Player:    {playerCards}");

    var bet = new HeadsUpBet(5, 5, 5, 0);

    var winnings = HeadsUpHoldem.CalculateWin(headsUpResult, bet);
    Console.WriteLine($"  Winnings:  ${winnings}");

    totalBet += bet.Total;
    totalWin += winnings;
    
    Console.WriteLine();
}

Console.WriteLine($"Summary: ${totalWin} / ${totalBet} ({Math.Round(totalWin * 100 / totalBet, 0)}%)");