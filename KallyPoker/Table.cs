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

    public void DealCards(Span<Card> cards)
    {
        DealCardsToPlayers(cards);
        Flop = new CardCollection(cards[11..14]);
        Turn = new CardCollection(cards[15].Bits);
        River = new CardCollection(cards[17].Bits);
    }

    private void DealCardsToPlayers(Span<Card> cards)
    {
        Players[0].Cards = new CardCollection(cards[0].Bits | cards[5].Bits);
        Players[1].Cards = new CardCollection(cards[1].Bits | cards[6].Bits);
        Players[2].Cards = new CardCollection(cards[2].Bits | cards[7].Bits);
        Players[3].Cards = new CardCollection(cards[3].Bits | cards[8].Bits);
        Players[4].Cards = new CardCollection(cards[4].Bits | cards[9].Bits);
    }
}