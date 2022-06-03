using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using AlphaVantageAPI;
using DryIoc.ImTools;
using Expert.DesktopApp.MVVM.Commands;
using JetBrains.Annotations;

namespace Expert.DesktopApp.UserControls;

public partial class PortfolioControl : UserControl, INotifyPropertyChanged
{
    private Portfolio _currentPortfolio;
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    public ICommand LeftCommand { get; set; }
    public ICommand RightCommand { get; set; }
    private int i = 0; //bad

    public List<string> Symbols { get; set; } = new List<string>
    {
        "IBM",
        "AAPL",
        "HPQ",
        "MSFT",
        "T"
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
            Name = "Тестовый портфель",
            BetaFactor = 0.5f,
            ProfitabilityIndicator = 10,
            Risk = 3.5341f,
            PossibleIncome = 0.41f,
            ValidityEndDate = DateTime.Now,
            StocksList = new List<Portfolio.PortfolioStock>()
            {
                new("T",1f)
            }
        });
        InitializeComponent();
        var x = Enumerable.Range(0, 1001).Select(i => i / 10.0).ToArray();
        var y = x.Select(v => Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v).ToArray();
        Linegraph.Plot(x,y);
        
        CurrentPortfolio = Portfolios[0];
        LeftCommand = new RelayCommand(_ => CurrentPortfolio = Portfolios[--i % Portfolios.Count]);
        RightCommand = new RelayCommand(_ => CurrentPortfolio = Portfolios[++i % Portfolios.Count]);
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