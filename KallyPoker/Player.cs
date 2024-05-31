using System.Runtime.CompilerServices;
using KallyPoker.PlayerTypes;

namespace KallyPoker;

public struct Player()
{
    public CardCollection Cards = CardCollection.Empty;
    public ulong Money = 0;
    public int Id = 0;
    public PlayerType? PlayerType;

    public Player(int id, CardCollection cards) : this()
    {
        Id = id;
        Cards = cards;
    }

    public void ResetCards()
    {
        Cards = CardCollection.Empty;
    }
}