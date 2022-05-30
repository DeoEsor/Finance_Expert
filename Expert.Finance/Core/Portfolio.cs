namespace AlphaVantageAPI;

public class Portfolio
{
    public List<(StockData, int)> StocksList { get; set; } = new List<(StockData, int)>();
    
    public int OwnerId { get; set; }
    
    public float Risk { get; set; }
    public float BetaFactor { get; set; }
    public float ProfitabilityIndicator { get; set; }
    
    public float PossibleIncome { get; set; }
    public DateTime ValidityEndDate { get; set; }
}