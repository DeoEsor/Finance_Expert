using AlphaVantageAPI;

namespace Expert.Finance.Extensions;

public static class StockDataExtensions
{
    public static Dictionary<string, List<decimal>> GetCloseData(this IEnumerable<StockData> data)
    {
        var dataByStock = new Dictionary<string, List<decimal>>();
        foreach (var stockData in data)
        {
            if (dataByStock.ContainsKey(stockData.Symbol))
                dataByStock[stockData.Symbol].Add(stockData.Close);
            else
                dataByStock.Add(stockData.Symbol, new List<decimal> { stockData.Close });
        }

        return dataByStock;
    }
}