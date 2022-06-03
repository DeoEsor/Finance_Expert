using System;
using System.Linq;
using System.Collections.Generic;

public static class StandardDeviationExtensions
{
    public static float StandardDeviation(this IEnumerable<float> values)
    {
        var avg = values.Average();
        return MathF.Sqrt(values.Average(v=>MathF.Pow(v-avg,2)));
    }
    
    public static decimal StandardDeviation(this IEnumerable<decimal> values)
    {
        var avg = values.Average();
        return (decimal)MathF.Sqrt((float)values.Average(v=>Math.Pow((double)(v-avg),2)));
    }
}