namespace KallyPoker;

public class HeadsUpHoldem
{
    public HeadsUpResult CheckHand(CardCollection communityCards, CardCollection dealerCards, CardCollection playerCards)
    {
        return new HeadsUpResult(
            HandChecker.GetBestHand(CardCollection.Union(communityCards, dealerCards)),
            HandChecker.GetBestHand(CardCollection.Union(communityCards, playerCards)));
    }
}