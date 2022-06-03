
using ExpertService;
using ExpertService.Messages;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace Expert.gRPC.Client;

public class ExpertClient 
{
    private ExpertService.KnowledgeBase.KnowledgeBaseClient Client { get; init; }
    private readonly ILogger<ExpertClient> _logger;
    private readonly GrpcChannel Channel;

    public List<AlphaVantageAPI.Portfolio> Portfolios { get; set; } = new List<AlphaVantageAPI.Portfolio>();
    
    public ExpertClient(ILogger<ExpertClient> logger = null)
    {
        _logger = logger;

        var httpHandler = new HttpClientHandler();

        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
		
        Channel  = GrpcChannel.ForAddress("https://localhost:7260", 
            new GrpcChannelOptions { HttpHandler = httpHandler });

        Client = new KnowledgeBase.KnowledgeBaseClient(Channel);
    }
    public  Portfolios CreatePortfolioByStocks(IEnumerable<string?> request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var req = new Stocks();
        foreach (var s in request)
        {
            if (s == null) continue;
            req.Stocks_.Add(new Stock{ Symbol = s});
        }

        Portfolios = new List<AlphaVantageAPI.Portfolio>();

        var result = Client.CreatePortfolioByStocks(req, headers, deadline, cancellationToken);

        foreach (var portfolio in result.Portfolios_)
        {
            var temp = new AlphaVantageAPI.Portfolio
            {
                PossibleIncome = portfolio.PossibleIncome,
                Risk = portfolio.Risk,
                ProfitabilityIndicator = portfolio.PossibleIncome,
                OwnerID = portfolio.OwnerId,
            };
            foreach (var item in portfolio.Stocks)
                temp.StocksList.Add(new AlphaVantageAPI.Portfolio.PortfolioStock(item.Stock.Symbol,item.Part));
            
            Portfolios.Add(temp);
        }

        return result;
    }

    public  Portfolios GetPortfoliosOfExpert(UserId request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return Client.GetPortfoliosOfExpert(request, headers, deadline, cancellationToken);
    }

    public  Portfolios GetPortfoliosOfUser(UserId request, Metadata headers = null, DateTime? deadline = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return Client.GetPortfoliosOfUser(request, headers, deadline, cancellationToken);
    }
}