using AlphaVantageAPI;
using Expert.Finance.Markowitz;
using ExpertService;
using ExpertService.Messages;
using Grpc.Core;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using Portfolio = AlphaVantageAPI.Portfolio;

namespace Expert.gRPC.Server.Services;

public class Expert_Service : ExpertService.KnowledgeBase.KnowledgeBaseBase
{
    private KnowledgeDb Db { get; set; }

    public Expert_Service(KnowledgeDb db)
    {
        Db = db;
    }
    
    public override async Task<Portfolios> CreatePortfolioByStocks(Stocks request, ServerCallContext context)
    {
        var result = new Portfolios();

        var stockData = new List<StockData>();
        var avConnection = new AVConnection(AVConnection.Apikey);

        foreach (var stock in request.Stocks_)
            stockData.AddRange(avConnection.GetWeeklyStocksDates(stock.Symbol));

        var portfolios = MarkowitzPortfolio.CreatePortfolios(stockData);
        
        Db.Portfolios.AddRange(portfolios);

        foreach (var portfolio in portfolios)
        {
            var temp = new ExpertService.Messages.Portfolio
            {
                Beta = portfolio.BetaFactor,
                OwnerId = 0,
                PossibleIncome = portfolio.PossibleIncome,
                Risk = portfolio.Risk,
            };
            foreach (var stockPart in portfolio.StocksList)
            {
                temp.Stocks.Add(new OwningStock
                {
                    Part = stockPart.Part,
                    Stock = new Stock
                    {
                        Symbol = stockPart.Stock
                    }
                });
            }
                
            result.Portfolios_.Add(temp);
        }
        return result;
    }

    public override async Task<Portfolios> GetPortfoliosOfExpert(UserId request, ServerCallContext context)
    {
        var result = new Portfolios();

        if (!Db.Portfolios.Any(s => s.OwnerID == request.Id)) return result;
            
        var portfolios = Db.Portfolios.Where(s => s.OwnerID == request.Id);

        foreach (var portfolio in portfolios)
        {
            var temp = new ExpertService.Messages.Portfolio
            {
                Beta = portfolio.BetaFactor,
                OwnerId = 0,
                PossibleIncome = portfolio.PossibleIncome,
                Risk = portfolio.Risk,
            };
            foreach (var stockPart in portfolio.StocksList)
            {
                temp.Stocks.Add(new OwningStock
                {
                    Part = stockPart.Part,
                    Stock = new Stock
                    {
                        Symbol = stockPart.Stock
                    }
                });
            }
                
            result.Portfolios_.Add(temp);
        }
        return result;
    }

    public override  async Task<Portfolios> GetPortfoliosOfUser(UserId request, ServerCallContext context)
    {
        var result = new Portfolios();

        if (!Db.Portfolios.Any(s => s.OwnerID == request.Id)) return result;
            
        var portfolios = Db.Portfolios.Where(s => s.OwnerID == request.Id);

        foreach (var portfolio in portfolios)
        {
            var temp = new ExpertService.Messages.Portfolio
            {
                Beta = portfolio.BetaFactor,
                OwnerId = 0,
                PossibleIncome = portfolio.PossibleIncome,
                Risk = portfolio.Risk,
            };
            foreach (var stockPart in portfolio.StocksList)
            {
                temp.Stocks.Add(new OwningStock
                {
                    Part = stockPart.Part,
                    Stock = new Stock
                    {
                        Symbol = stockPart.Stock
                    }
                });
            }
                
            result.Portfolios_.Add(temp);
        }
        return result;
    }
}