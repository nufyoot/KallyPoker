using System.Numerics;

namespace KallyPoker;

public struct CardCollection
{
    public ulong Value;

    public static readonly CardCollection FullDeck = new(Suit.ClubsMask | Suit.DiamondsMask | Suit.HeartsMask | Suit.SpadesMask);
    
    public CardCollection(ulong value)
    {
        Value = value;
    }

    public CardCollection(ReadOnlySpan<char> cards)
    {
        var start = 0;
        for (var i = 0; i < cards.Length; i++)
        {
            if (cards[i] != ',') continue;
            
            if (i - start == 2)
                Value |= new Card(cards.Slice(start, 2));
            else
                throw new ArgumentException($"The card value '{cards.Slice(start, i - start)}' is not valid. Expected 2 characters.");

            start = i + 1;
        }

        if (start != cards.Length)
        {
            if (cards.Length - start != 2)
                throw new ArgumentException($"The card value '{cards[start..]}' is not valid. Expected 2 characters.");
            Value |= new Card(cards[start..]);
        }
    }

    public static CardCollection operator |(CardCollection cards, Card card) => new(cards.Value | card.Value);

    public void Add(Card card) => Value |= card;
    public void Remove(Card card) => Value &= ~card;
    public bool Contains(Card card) => (Value & card) != 0;

    public Card[] ToArray()
    {
        var count = BitOperations.PopCount(Value);
        var result = new Card[count];

        var resultIndex = 0;
        for (var cardMask = 1UL << 63; cardMask != 0; cardMask = cardMask >> 1)
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