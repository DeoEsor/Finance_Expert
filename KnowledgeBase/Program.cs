using AlphaVantageAPI;
using KnowledgeBase;

public class Program
{
    public static void Main()
    {
        var api = "SXNME3YSQZTFJK7I";

        using var db = new KnowledgeDb();
        
        var avConnection = new AVConnection(api);
            
        db.Stocks.AddRange(avConnection.GetMonthlyStocksDates("IBM"));
        db.SaveChanges(); 
    }
}