using System.Numerics;
using System.Runtime.CompilerServices;

namespace KallyPoker;

public struct CardCollection(ulong value)
{
    public ulong Value = value;

    public static readonly CardCollection FullDeck = new(Suit.ClubsMask | Suit.DiamondsMask | Suit.HeartsMask | Suit.SpadesMask);
    public static readonly CardCollection Empty = new(0);

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

    public bool IsEmpty => Value == 0;

    public void Add(Card card) => Value |= card.Value;
    public void Add(CardCollection cards) => Value |= cards.Value;
    public void Remove(Card card) => Value &= ~card.Value;
    public bool Contains(Card card) => (Value & card.Value) != 0;
    public int Count => BitOperations.PopCount(Value);

    public CardCollection Except(Hand hand) => new(Value & ~hand.CardCollection.Value);
    public CardCollection Except(CardCollection cards) => new(Value & ~cards.Value);
    public CardCollection Filter(Suit suit) => new(Value & suit.Mask);
    public CardCollection Filter(Face face) => new(Value & face.Mask);
    public CardCollection Intersect(CardCollection other) => new(Value & other.Value);
    public CardCollection GetClubs() => Filter(Suit.Clubs);
    public CardCollection GetDiamonds() => Filter(Suit.Diamonds);
    public CardCollection GetHearts() => Filter(Suit.Hearts);
    public CardCollection GetSpades() => Filter(Suit.Spades);

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
            var clubFaceValue = ((Value >> Suit.ClubsBitShift) & cardMask) << Suit.ClubsBitShift;
            var diamondFaceValue = ((Value >> Suit.DiamondsBitShift) & cardMask) << Suit.DiamondsBitShift;
            var heartFaceValue = ((Value >> Suit.HeartsBitShift) & cardMask) << Suit.HeartsBitShift;
            var spadeFaceValue = ((Value >> Suit.SpadesBitShift) & cardMask) << Suit.SpadesBitShift;
            
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