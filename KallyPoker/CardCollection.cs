using System.Numerics;

namespace KallyPoker;

public struct CardCollection(ulong value)
{
    public ulong Value = value;

    public static readonly CardCollection FullDeck = new(Suit.ClubsMask | Suit.DiamondsMask | Suit.HeartsMask | Suit.SpadesMask);

    public static ErrorTuple<CardCollection> Parse(ReadOnlySpan<char> cards)
    {
        var start = 0;
        var value = 0UL;
        for (var i = 0; i < cards.Length; i++)
        {
            if (cards[i] != ',') continue;

            if (i - start == 2)
            {
                var card = Card.Parse(cards.Slice(start, 2));
                if (card.HasError)
                    return card.Error;
                value |= card.Result.Value;
            }
            else
                return new Error($"The card value '{cards.Slice(start, i - start)}' is not valid. Expected 2 characters.");

            start = i + 1;
        }

        if (start != cards.Length)
        {
            if (cards.Length - start != 2)
                return new Error($"The card value '{cards[start..]}' is not valid. Expected 2 characters.");
            var card = Card.Parse(cards[start..]);
            if (card.HasError)
                return card.Error;
            value |= card.Result.Value;
        }

        return new CardCollection(value);
    }

    public bool IsEmpty() => Value == 0;

    public void Add(Card card) => Value |= card.Value;
    public void Remove(Card card) => Value &= ~card.Value;
    public bool Contains(Card card) => (Value & card.Value) != 0;

    public Card[] ToArray()
    {
        var count = BitOperations.PopCount(Value);
        var result = new Card[count];

        var resultIndex = 0;
        for (var cardMask = 1UL << 63; cardMask != 0; cardMask >>= 1)
        {
            var cardValue = Value & cardMask;
            if (cardValue != 0)
                result[resultIndex++] = new Card(cardValue);
        }

        return result;
    }

    public override string ToString()
    {
        return string.Join(",", ToArray().Select(c => c.ToString()));
    }
}