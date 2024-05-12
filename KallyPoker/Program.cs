// See https://aka.ms/new-console-template for more information

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

    var cardPosition = 0;
    
    // Deal the first card.
    players[0].AddCard(cards[cardPosition++]);
    players[1].AddCard(cards[cardPosition++]);
    players[2].AddCard(cards[cardPosition++]);
    players[3].AddCard(cards[cardPosition++]);
    players[4].AddCard(cards[cardPosition++]);
    
    // Deal the second card.
    players[0].AddCard(cards[cardPosition++]);
    players[1].AddCard(cards[cardPosition++]);
    players[2].AddCard(cards[cardPosition++]);
    players[3].AddCard(cards[cardPosition++]);
    players[4].AddCard(cards[cardPosition++]);
    
    // Deal the flop
    table.AddCard(cards[cardPosition++]);
    table.AddCard(cards[cardPosition++]);
    table.AddCard(cards[cardPosition++]);
    
    // Burn a card
    cardPosition++;
    
    // Deal the turn
    table.AddCard(cards[cardPosition++]);
    
    // Burn a card
    cardPosition++;
    
    // Deal the river
    table.AddCard(cards[cardPosition++]);
}

stopwatch.Stop();
Console.WriteLine($"The entire process took: {stopwatch.ElapsedMilliseconds} ms");