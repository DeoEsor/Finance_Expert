namespace Expert.Finance.Extensions;

public static class MatrixExtensions
{
    public static double[,] Multiplication(this double[,] a, double[,] b)
    {
        if (a.GetLength(1) != b.GetLength(0)) 
            throw new ArgumentException("Матрицы нельзя перемножить");
        
        var r = new double[a.GetLength(0), b.GetLength(1)];
        for (var i = 0; i < a.GetLength(0); i++)
        for (var j = 0; j < b.GetLength(1); j++)
        for (var k = 0; k < b.GetLength(0); k++)
            r[i,j] += a[i,k] * b[k,j];
        return r;
    }
}