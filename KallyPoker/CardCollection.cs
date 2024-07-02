using System.Numerics;

namespace KallyPoker;

public readonly struct CardCollection
{
    private readonly ulong _value;

    public static readonly CardCollection FullDeck = new(Suit.ClubsMask | Suit.DiamondsMask | Suit.HeartsMask | Suit.SpadesMask);
    public static readonly CardCollection Empty = new(0);

    public CardCollection(ulong value)
    {
        _value = value;
    }

    public static CardCollection Union(CardCollection first, CardCollection second) => new(first._value | second._value);

    public static ErrorTuple<CardCollection> Parse(ReadOnlySpan<char> cards)
    {
        var value = 0UL;
        for (var i = 0; i < cards.Length; i += 3)
        {
            var card = Card.Parse(cards.Slice(i, 2));
            if (card.HasError)
                return card.Error;
            value |= card.Result;
        }

        return new CardCollection(value);
    }
    
    public CardCollection Filter(Suit suit) => new(_value & suit.Mask);
    public CardCollection Filter(Face face) => new(_value & face.Mask);
    public bool IsEmpty => _value == 0;

    public CardCollection FacesOnly() =>
        new(
            ((_value & Suit.ClubsMask) >> Suit.ClubsBitShift) |
            ((_value & Suit.DiamondsMask) >> Suit.DiamondsBitShift) |
            ((_value & Suit.HeartsMask) >> Suit.HeartsBitShift) |
            ((_value & Suit.SpadesMask) >> Suit.SpadesBitShift));
    
    public int Count => BitOperations.PopCount(_value);
    
    public CardCollection Except(CardCollection cards) => new(_value & ~cards._value);
    
    public CardCollection Intersect(CardCollection other) => new(_value & other._value);

    public Card GetTopCard()
    {
        Span<Card> cardHolder = stackalloc Card[1];
        CopyTo(cardHolder, 1);
        return cardHolder[0];
    }

    public Card GetTopCard(Face face)
    {
        Span<Card> cardHolder = stackalloc Card[1];
        Filter(face).CopyTo(cardHolder, 1);
        return cardHolder[0];
    }

    public void CopyTo(Span<Card> cards, int maxLength = -1)
    {
        if (maxLength == -1)
            maxLength = Count;
        
        var resultIndex = 0;
        for (var cardMask = 1UL << 12; cardMask != 0; cardMask >>= 1)
        {
            var clubFaceValue = ((_value >> Suit.ClubsBitShift) & cardMask) << Suit.ClubsBitShift;
            var diamondFaceValue = ((_value >> Suit.DiamondsBitShift) & cardMask) << Suit.DiamondsBitShift;
            var heartFaceValue = ((_value >> Suit.HeartsBitShift) & cardMask) << Suit.HeartsBitShift;
            var spadeFaceValue = ((_value >> Suit.SpadesBitShift) & cardMask) << Suit.SpadesBitShift;
            
            if (clubFaceValue != 0)
                cards[resultIndex++] = new Card(clubFaceValue);
            if (resultIndex == maxLength)
                return;
            
            if (diamondFaceValue != 0)
                cards[resultIndex++] = new Card(diamondFaceValue);
            if (resultIndex == maxLength)
                return;
            
            if (heartFaceValue != 0)
                cards[resultIndex++] = new Card(heartFaceValue);
            if (resultIndex == maxLength)
                return;
            
            if (spadeFaceValue != 0)
                cards[resultIndex++] = new Card(spadeFaceValue);
            if (resultIndex == maxLength)
                return;
        }
    }
    
    public Card[] ToArray()
    {
        var result = new Card[Count];
        CopyTo(result);
        return result;
    }

    public override string ToString()
    {
        return string.Join(",", ToArray().Select(c => c.ToString()));
    }
}