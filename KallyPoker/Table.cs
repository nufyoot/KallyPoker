namespace KallyPoker;

public struct Table()
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

        const int flopStart = (PokerConstants.PlayerCount * 2) + 1;
        const int turnStart = flopStart + 4;
        const int riverStart = turnStart + 2;
        Flop = new CardCollection(cards.Slice(flopStart, 3));
        Turn = new CardCollection(cards[turnStart]);
        River = new CardCollection(cards[riverStart]);
    }

    private void DealCardsToPlayers(ReadOnlySpan<Card> cards)
    {
        for (var i = 0; i < PokerConstants.PlayerCount; i++)
            Players[i].Cards = cards[i] + cards[PokerConstants.PlayerCount + i];
    }
}