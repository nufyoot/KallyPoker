using System.Runtime.CompilerServices;

namespace KallyPoker;

[InlineArray(PokerConstants.PlayerCount)]
public struct PlayerCollection
{
    private Player _element0;

    public void ResetCards()
    {
        foreach (ref var player in this)
            player.ResetCards();
    }
}