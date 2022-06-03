namespace MathOptimization;
using System.Collections.Generic;

public class Simplex
{
    
    private double[,] _table;

    private readonly int _m, _n;

    private readonly List<int> _basis;

    public Simplex(double[,] source)
    {
        _m =source.GetLength(0);
        _n = source.GetLength(1);
        _table = new double[_m, _n + _m - 1];
        _basis = new List<int>();

        for (var i = 0; i < _m; i++)
        {
            for (var j = 0; j < _table.GetLength(1); j++)
                _table[i, j] = j < _n ? source[i, j] : 0;
            
            if (_n + i >= _table.GetLength(1)) continue;
            
            _table[i, _n + i] = 1;
            _basis.Add(_n + i);
        }

        _n = _table.GetLength(1);
    }
    
    public double[,] Calculate(double[] result)
    {
        int mainCol, mainRow; //ведущие столбец и строка

        while (!IsItEnd())
        {
            mainCol = FindMainCol();
            mainRow = FindMainRow(mainCol);
            _basis[mainRow] = mainCol;

            var newTable = new double[_m, _n];

            for (var j = 0; j < _n; j++)
                newTable[mainRow, j] = _table[mainRow, j] / _table[mainRow, mainCol];

            for (var i = 0; i < _m; i++)
            {
                if (i == mainRow)
                    continue;

                for (var j = 0; j < _n; j++)
                    newTable[i, j] = _table[i, j] - _table[i, mainCol] * newTable[mainRow, j];
            }
            _table = newTable;
        }
        
        for (var i = 0; i < result.Length; i++)
        {
            var k = _basis.IndexOf(i + 1);
            if (k != -1)
                result[i] = _table[k, 0];
            else
                result[i] = 0;
        }

        return _table;
    }

    private bool IsItEnd()
    {
        var flag = true;

        for (var j = 1; j < _n; j++)
        {
            if (!(_table[_m - 1, j] < 0)) continue;
            
            flag = false;
            break;
        }

        return flag;
    }

    private int FindMainCol()
    {
        var mainCol = 1;

        for (var j = 2; j < _n; j++)
            if (_table[_m - 1, j] < _table[_m - 1, mainCol])
                mainCol = j;

        return mainCol;
    }

    private int FindMainRow(int mainCol)
    {
        var mainRow = 0;

        for (var i = 0; i < _m - 1; i++)
            if (_table[i, mainCol] > 0)
            {
                mainRow = i;
                break;
            }

        for (var i = mainRow + 1; i < _m - 1; i++)
            if ((_table[i, mainCol] > 0) && 
                ((_table[i, 0] / _table[i, mainCol]) < (_table[mainRow, 0] / _table[mainRow, mainCol])))
                mainRow = i;

        return mainRow;
    }
}
