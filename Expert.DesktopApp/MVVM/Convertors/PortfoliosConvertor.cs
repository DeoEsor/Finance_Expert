using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using AlphaVantageAPI;
using Google.Protobuf.Reflection;

namespace Expert.DesktopApp.MVVM.Convertors;

public class PortfoliosConvertor : IValueConverter
{
        
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
                if (!(values is ObservableCollection<Portfolio> portfolios)) return null;
                //List<(StockData, int)> a = portfolios.SelectMany(s => s.StocksList).ToList();
                //return a.SelectMany<(StockData, int),StockData>(s => new []{s.Item1}).ToList();
                return null!; // TODO mb
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
                throw new NotImplementedException();
        }
}