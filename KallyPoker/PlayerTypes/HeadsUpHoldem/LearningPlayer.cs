namespace KallyPoker.PlayerTypes.HeadsUpHoldem;

public class LearningPlayer
{
    private int nextHandTripsPlus = 0;
    private int nextHandPocketBonus = 0;
    private int nextHandAnteOdds = 0;
    public HeadsUpBet DecideBet(Hand communityHand, CardCollection playerCards)
    {
        var flopCards = new CardCollection(communityHand[0] | communityHand[1] | communityHand[2]);
        var communityCards = new CardCollection(communityHand);
        var preFlopHand = HandChecker.GetBestHand(playerCards);
        var postFlopHand = HandChecker.GetBestHand(CardCollection.Union(playerCards, flopCards));
        var finalHand = HandChecker.GetBestHand(CardCollection.Union(playerCards, communityCards));
    }
}