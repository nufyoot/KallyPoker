using System.Diagnostics;
using KallyPoker;

var cardCollection = CardCollection.FullDeck;
var cards = cardCollection.ToArray();
var players = new Player[5];
var table = new Table();
var random = new Random();
var startingMoney = 100U;

var stopwatch = Stopwatch.StartNew();

for (var i = 0; i < 1_000_000; i++)
{
    // Reset the game
    players[0].Reset(startingMoney);
    players[1].Reset(startingMoney);
    players[2].Reset(startingMoney);
    players[3].Reset(startingMoney);
    players[4].Reset(startingMoney);
    table.Reset();
    
    random.Shuffle(cards);
    
    // Deal the first card.
    players[0].AddCard(cards[0]);
    players[1].AddCard(cards[1]);
    players[2].AddCard(cards[2]);
    players[3].AddCard(cards[3]);
    players[4].AddCard(cards[4]);
    
    // Deal the second card.
    players[0].AddCard(cards[5]);
    players[1].AddCard(cards[6]);
    players[2].AddCard(cards[7]);
    players[3].AddCard(cards[8]);
    players[4].AddCard(cards[9]);
    
    // Deal the flop
    table.AddCard(cards[10]);
    table.AddCard(cards[11]);
    table.AddCard(cards[12]);
    
    // Deal the turn
    table.AddCard(cards[13]);
    
    // Deal the river
    table.AddCard(cards[14]);
}

stopwatch.Stop();
Console.WriteLine($"The entire process took: {stopwatch.ElapsedMilliseconds} ms");