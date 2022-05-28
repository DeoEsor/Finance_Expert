using AlphaVantageAPI;

namespace KnowledgeBase;
using Microsoft.EntityFrameworkCore;

/*

 */
public sealed class KnowledgeDb : DbContext, IKnowledgeHolder<StockData>
{
    /// <summary>
    /// 
    /// </summary>
    public DbSet<StockData> Stocks => Set<StockData>();
    
    
    public KnowledgeDb() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder
            .UseSqlServer(@"Server=localhost,1433;Database=KnowledgeBase;Trusted_Connection=False;User ID=sa;Password=Pa55w0rd");

    public IEnumerable<StockData> GetKnowledge() => Stocks;
}