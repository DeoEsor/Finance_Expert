namespace AlphaVantageAPI;
// ReSharper disable InconsistentNaming

public class AVQuery 
{
    private readonly string apiKey;
    private readonly string symbol;
    private readonly string function; // TODO to enum
    private readonly string datatype = "csv"; // TODO to enum

    protected AVQuery(string apiKey, string function, string symbol, string datatype = "csv")
    {
        this.apiKey = apiKey;
        this.function = function;
        this.symbol = symbol;
        this.datatype = datatype;
    }

    
    public static AVQuery CreateAvQuery(string api, string function, string sym, string type = "csv") 
        => new AVQuery(api, function, sym, type);

    public override string ToString() 
        =>  "https://" +
            @"www.alphavantage.co/query?" +
            $@"function={function}" + 
            $@"&symbol={symbol}" + 
            $@"&apikey={apiKey}" + 
            $@"&datatype={datatype}";
}