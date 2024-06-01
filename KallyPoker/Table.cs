namespace KallyPoker;

public struct Table(Random random)
{
    public CardCollection Flop = CardCollection.Empty;
    public CardCollection Turn = CardCollection.Empty;
    public CardCollection River = CardCollection.Empty;
    public PlayerCollection Players = new();

    public void Reset()
    {
        Flop = CardCollection.Empty;
        Turn = CardCollection.Empty;
        River = CardCollection.Empty;
        Players.ResetCards();
    }
    
    public CardCollection CardsAtFlop => Flop;
    public CardCollection CardsAtTurn => CardCollection.Union(Flop, Turn);
    public CardCollection CardsAtRiver => CardCollection.Union(Flop, Turn, River);

    public void DealCards(ReadOnlySpan<Card> cards)
    {
        DealCardsToPlayers(cards);
        Flop = new CardCollection(cards[11..14]);
        Turn = new CardCollection(cards[15]);
        River = new CardCollection(cards[17]);
    }

    private void DealCardsToPlayers(ReadOnlySpan<Card> cards)
    {
        Players[0].Cards = cards[0] + cards[5];
        Players[1].Cards = cards[1] + cards[6];
        Players[2].Cards = cards[2] + cards[7];
        Players[3].Cards = cards[3] + cards[8];
        Players[4].Cards = cards[4] + cards[9];
    }
}