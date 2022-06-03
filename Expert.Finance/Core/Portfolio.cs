namespace AlphaVantageAPI;

public class Portfolio
{
    public class PortfolioStock
    {
        public string Stock { get; set; }
        public float Part { get; set; }

        public PortfolioStock(string stock, float part)
        {
            Stock = stock;
            Part = part;
        }
    }
    public List<PortfolioStock> StocksList { get; set; } = new List<PortfolioStock>();
    
    public int Id { get; set; }
    public int OwnerID { get; set; } = 0;

    public string Name { get; set; } = string.Empty;
    
    public float Risk { get; set; }
    public float BetaFactor { get; set; }
    public float ProfitabilityIndicator { get; set; }
    
    public float PossibleIncome { get; set; }
    public DateTime ValidityEndDate { get; set; }
}