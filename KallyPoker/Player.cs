namespace KallyPoker;

public struct Player(CardCollection cards)
{
    public CardCollection Cards = cards;

    public void AddCard(Card card)
    {
        Cards |= card;
    }
}