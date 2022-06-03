using AlphaVantageAPI;
using Expert.Finance.Extensions;
using MathOptimization;
using static System.MathF;
namespace Expert.Finance.Markowitz;

public static class MarkowitzPortfolio
{
    #region Utils

    private static double[,] InitSLAUTableMax(List<float> profit)
    {
        var k = profit.Count;
        var a = new double[2, k + 1];
        for (var i = 0; i < k + 1; i++)
            a[0, i] = 1;
        a[1, 0] = 0;
        for (var i = 0; i < k; i++)
            a[1, i + 1] = profit[i];
        return a;
    }
    private static float GetRiskByAverage(Portfolio randomPortfolio, double[,] covMatrix)
    {
        var partsMatrix = new double[1, randomPortfolio.StocksList.Count];
        var partsMatrixT = new double[randomPortfolio.StocksList.Count, 1];

        for (var i = 0; i < randomPortfolio.StocksList.Count; i++)
            partsMatrixT[i, 0] = partsMatrix[0, i] = randomPortfolio.StocksList[i].Part;
        
        return Sqrt((float)partsMatrix.Multiplication(covMatrix).Multiplication(partsMatrixT)[0, 0]); // 1xN * NxN * Nx1 = 1x1
    }

    private static double[,] GetClosesMatrix(Dictionary<string, List<decimal>> dataByStock)
    {
        var iterator = 0;
        var closesData = new double[dataByStock.Keys.Count, dataByStock.First().Value.Count];
        foreach (var sym in dataByStock.Keys)
        {
            for (var i = 0; i < dataByStock[sym].Count; i++)
                closesData[iterator, i] = (double)dataByStock[sym][i];
            iterator++;
        }

        return closesData;
    }

    private static Dictionary<string, List<decimal>> IncomeByStock(Dictionary<string, List<decimal>> dataByStock)
    {
        var incomeByStock = new Dictionary<string, List<decimal>>();
        foreach (var symbol in dataByStock.Keys)
        {
            incomeByStock.Add(symbol, new List<decimal>());
            for (var i = 1; i < dataByStock.First().Value.Count; i++)
                incomeByStock[symbol]
                    .Add((decimal)Log((float)(dataByStock[symbol][i] / dataByStock[symbol][i - 1]))); // loss of accuracy
        }

        return incomeByStock;
    }

    #endregion
    
    public static IEnumerable<Portfolio> CreatePortfolios(IEnumerable<StockData> data)//, float minProfit, float maxRisk
    {
        var result = new List<Portfolio>();
        
        var stockClosesData = data.GetCloseData();

        var incomeByStock = IncomeByStock(stockClosesData);

        var expectedProfitability = 
            incomeByStock.Keys
                    .ToDictionary(symbol => symbol, symbol => (float)incomeByStock[symbol].Average());
        var riskByStock = incomeByStock.Keys
                    .ToDictionary(sym => sym, sym => incomeByStock[sym].StandardDeviation());
        
        var closesMatrix = GetClosesMatrix(stockClosesData);

        alglib.covm(closesMatrix, out var covMatrix);


        using var random = RandomExtensions
            .GetRandomValuesWithSum(0, 1, expectedProfitability.Keys.Count)
            .GetEnumerator();

        var randomPortfolio = new Portfolio();

        foreach (var stocks in expectedProfitability.Keys)
        {
            randomPortfolio.StocksList.Add(new Portfolio.PortfolioStock(stocks,random.Current));
            random.MoveNext();
        }

        randomPortfolio.PossibleIncome = randomPortfolio.StocksList
            .Sum(s => expectedProfitability[s.Stock] * s.Part);
        
        randomPortfolio.Risk = GetRiskByAverage(randomPortfolio, covMatrix);

        var max = MaxProfitPortfolio(randomPortfolio.StocksList.Select(s => s.Part).ToList());
        
        var maxProfitPortfolio = new Portfolio();

        foreach (var stocks in expectedProfitability.Keys)
        {
            maxProfitPortfolio.StocksList.Add(new Portfolio.PortfolioStock(stocks,random.Current));
            random.MoveNext();
        }

        for (int i = 0; i < max.Count; i++)
            maxProfitPortfolio.StocksList[i].Part = max[i];

        maxProfitPortfolio.PossibleIncome = randomPortfolio.StocksList
            .Sum(s => expectedProfitability[s.Stock] * s.Part);
        
        maxProfitPortfolio.Risk = GetRiskByAverage(randomPortfolio, covMatrix);
        
        result.Add(randomPortfolio);
        result.Add(maxProfitPortfolio);
        
        return result;
    }
    
    private static List<float> MaxProfitPortfolio(List<float> parts)
    {
        double[,] table = InitSLAUTableMax(parts);
 
        var result = new double[parts.Count];
        
        var S = new Simplex(table);
        S.Calculate(result);

        return Enumerable.Cast<float>(result).ToList();
    }
}