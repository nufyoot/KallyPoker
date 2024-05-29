namespace KallyPoker;

public struct Table()
{
    public CardCollection Flop = CardCollection.Empty;
    public Card Turn = Card.Empty;
    public Card River = Card.Empty;

    public void Reset()
    {
        Flop = CardCollection.Empty;
        Turn = Card.Empty;
        River = Card.Empty;
    }

    public CardCollection Cards => new CardCollection(Flop.Value | Turn.Value | River.Value);
}