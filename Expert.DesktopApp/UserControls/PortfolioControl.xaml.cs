using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using AlphaVantageAPI;
using DryIoc.ImTools;
using JetBrains.Annotations;

namespace Expert.DesktopApp.UserControls;

public partial class PortfolioControl : UserControl, INotifyPropertyChanged
{
    private Portfolio _currentPortfolio;
    public ObservableCollection<Portfolio> Portfolios { get; set; } = new ObservableCollection<Portfolio>();

    public List<string> Symbols { get; set; } = new List<string>
    {
        "IBM",
        "AAPL",
        "HPQ"
    };

    public Portfolio CurrentPortfolio
    {
        get => _currentPortfolio;
        set
        {
            _currentPortfolio = value;
            OnPropertyChanged();
        }
    }

    public PortfolioControl()
    {
        Portfolios.Add(new Portfolio
        {
            OwnerId = 1,
            BetaFactor = 0.5f,
            ProfitabilityIndicator = 10,
            Risk = 0.7f,
            PossibleIncome = 0.8f,
            ValidityEndDate = DateTime.Now,
            StocksList = new List<(StockData, int)>
            {
                new(new StockData
                    {
                        Symbol = "IBM",
                        Open = 5,
                        Close = 4,
                        Date = DateTime.Now,
                        Id = 1,
                        Volume = 10
                    },
                    5)
                
            }
        });
        InitializeComponent();
        var x = Enumerable.Range(0, 1001).Select(i => i / 10.0).ToArray();
        var y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v).ToArray();
        linegraph.Plot(x,y);
        
        CurrentPortfolio = Portfolios[0];
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}