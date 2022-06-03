using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AlphaVantageAPI;

public class StockData
{
    public DateTime Date { get; set; }
    
    [MaxLength(4)]
    public string Symbol { get; set; } = "";
    public decimal Open { get; set; }

    public decimal High { get; set; }
    public decimal Low { get; set; }

    public decimal Close { get; set; }
    public int Volume { get; set; }


    public override string ToString() 
        => $"{Date}\t{Symbol}\t{Open}\t{High}\t{Low}\t{Close}\t{Volume}";
}