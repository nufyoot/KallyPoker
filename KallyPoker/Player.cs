namespace KallyPoker;

public struct Player(CardCollection cards)
{
    public CardCollection Cards = cards;
    public uint Money = 0;

    public void ResetMoney(uint money)
    {
        Money = money;
    }

    public void ResetCards()
    {
        Cards = CardCollection.Empty;
    }
    
    public void AddCard(Card card) => Cards.Add(card);
}