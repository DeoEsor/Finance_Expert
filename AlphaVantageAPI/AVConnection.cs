using System.Net;
using Microsoft.Data.Analysis;
using ServiceStack;
using ServiceStack.Text;
namespace AlphaVantageAPI;

public class AVConnection
{
    private readonly string _apiKey;
    
    public AVConnection(string apiKey) => _apiKey = apiKey;

    public IEnumerable<StockData> GetDailyPrices(string symbol)
    {
        var query = AVQuery.CreateAvQuery(_apiKey, "TIME_SERIES_DAILY", symbol);
        
        var prices = query
            .ToString()
            .GetStringFromUrl()
            .FromCsv<List<StockData>>();
        
        prices.ForEach(s => s.Symbol = symbol);
        return prices;
    }
}