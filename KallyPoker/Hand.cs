using System.Runtime.CompilerServices;
using System.Text;

namespace KallyPoker;

[InlineArray(5)]
public struct Hand
{
    private Card _element0;

    public Hand(Card card1, Card card2, Card card3, Card card4, Card card5)
    {
        this[0] = card1;
        this[1] = card2;
        this[2] = card3;
        this[3] = card4;
        this[4] = card5;
    }

    public override string ToString()
    {
        var result = new StringBuilder(14);
        result.Append(this[0].ToString());
        result.Append(',');
        result.Append(this[1].ToString());
        result.Append(',');
        result.Append(this[2].ToString());
        result.Append(',');
        result.Append(this[3].ToString());
        result.Append(',');
        result.Append(this[4].ToString());

        return result.ToString();
    }
}