namespace KallyPoker;

public ref struct HeadsUpBet(decimal anteAndOdds, decimal tripsPlus, decimal pocketBonus, decimal raise)
{
    public decimal Ante { get; set; } = anteAndOdds;
    public decimal Odds { get; set; } = anteAndOdds;
    public decimal TripsPlus { get; set; } = tripsPlus;
    public decimal PocketBonus { get; set; } = pocketBonus;
    public decimal Raise { get; set; } = raise;

    public decimal Total => Ante + Odds + TripsPlus + PocketBonus + Raise;

    public override string ToString()
    {
        return $"Ante/Odds: {Ante}, Trips Plus: {TripsPlus}, Pocket Bonus: {PocketBonus}, Raise: {Raise}, Total: {Total}";
    }
}