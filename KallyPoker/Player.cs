using System.Runtime.CompilerServices;

namespace KallyPoker;

public struct Player()
{
    public CardCollection Cards = CardCollection.Empty;
    public uint Money = 0;
    public int Id = 0;

    public void ResetCards()
    {
        Cards = CardCollection.Empty;
    }
}