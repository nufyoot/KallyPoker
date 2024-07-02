namespace KallyPoker;

public readonly ref struct HeadsUpResult(CardCollection communityCards, CardCollection dealerCards, CardCollection playerCards)
{
    public readonly HandResult DealerHandResult = HandChecker.GetBestHand(CardCollection.Union(communityCards, dealerCards));
    public readonly HandResult PlayerHandResult = HandChecker.GetBestHand(CardCollection.Union(communityCards, playerCards));

    public bool DealerQualifies => DealerHandResult.Rank >= HandRank.Pair;

    public bool PlayerWins => PlayerHandResult.CompareTo(DealerHandResult) > 0;

    public bool Tied => PlayerHandResult.CompareTo(DealerHandResult) == 0;

    public bool PlayerHasPocketPair => playerCards.FacesOnly().Count == 1;

    public bool PlayerHasPocketAces => playerCards.Filter(Face.Aces).Count == 2;

    public bool PlayerHasAceFace =>
        playerCards.Filter(Face.Aces).Count == 1 && (
            playerCards.Filter(Face.Kings).Count == 1 ||
            playerCards.Filter(Face.Queens).Count == 1 ||
            playerCards.Filter(Face.Jacks).Count == 1
        );
    
    public bool PlayerHasAceFaceSuited => 
        PlayerHasAceFace && (
            playerCards.Filter(Suit.Clubs).Count == 2 ||
            playerCards.Filter(Suit.Diamonds).Count == 2 ||
            playerCards.Filter(Suit.Hearts).Count == 2 ||
            playerCards.Filter(Suit.Spades).Count == 2
            );
}