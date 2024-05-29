using System.Runtime.CompilerServices;

namespace KallyPoker;

[InlineArray(5)]
public struct PlayerCollection
{
    private Player _element0;

    public void ResetCards()
    {
        this[0].ResetCards();
        this[1].ResetCards();
        this[2].ResetCards();
        this[3].ResetCards();
        this[4].ResetCards();
    }
}