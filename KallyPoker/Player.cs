namespace KallyPoker;

public struct Player(CardCollection cards)
{
    public CardCollection Cards = cards;
    public uint Money = 0;

    public void Reset(uint money)
    {
        Cards = new CardCollection(0);
        Money = money;
    }
    
    public void AddCard(Card card) => Cards.Add(card);
}