namespace KallyPoker;

public struct Table(CardCollection cards)
{
    public CardCollection Cards = cards;

    public void Reset()
    {
        Cards = new CardCollection(0);
    }
    
    public void AddCard(Card card)
    {
        Cards |= card;
    }
}