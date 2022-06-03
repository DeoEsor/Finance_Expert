using AlphaVantageAPI;
using KnowledgeBase;

public class Program
{
    public static void Main()
    {
        

        using var db = new KnowledgeDb();
        
        var avConnection = new AVConnection(AVConnection.Apikey);
            
        //db.Stocks.AddRange(avConnection.GetMonthlyStocksDates("IBM"));
        db.SaveChanges(); 
    }
    
}