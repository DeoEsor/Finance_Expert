using AlphaVantageAPI;

namespace KnowledgeBase;
using Microsoft.EntityFrameworkCore;

/*

 */
public sealed class KnowledgeDb : DbContext, IKnowledgeHolder<Portfolio>
{
    
    public DbSet<Portfolio> Portfolios => Set<Portfolio>();
    
    
    public KnowledgeDb() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder
            .UseSqlServer(@"Server=localhost,1433;Database=KnowledgeBase;Trusted_Connection=False;User ID=sa;Password=Pa55w0rd");

    IEnumerable<Portfolio> IKnowledgeHolder<Portfolio>.GetKnowledge() => Portfolios;
}