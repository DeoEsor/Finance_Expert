using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using AlphaVantageAPI;
using DryIoc;
using Expert.Core.Models;
using Expert.DesktopApp.MVVM.Commands;
using Expert.DesktopApp.UserControls;
using Expert.gRPC.Client;
using JetBrains.Annotations;
using UserService.Messages;

namespace Expert.DesktopApp.MVVM.ViewModel;

public class MainViewModel : INotifyPropertyChanged
{
    public UserService.Messages.User User { get; set; } = new UserService.Messages.User()
    {
        Username = "User",
        Status = UserStatus.Consumer
    };
    
    public ICommand MainViewCommand { get; set; }
    public ICommand DiscoveryCommand { get; set; }
    public ICommand CreatePortfolioCommand { get; set; }
    public ICommand AddPortfolioCommand { get; set; }

    public object Content
    {
        get => _content;
        set
        {
            _content = value;
            OnPropertyChanged();
        }
    }

    public Visibility IsExpert => User.Status == UserStatus.Expert ? Visibility.Visible : Visibility.Hidden;
    private readonly PortfolioControl _portfolioControl = new PortfolioControl();
    private readonly DiscoveryView _discoveryViewControl = new DiscoveryView();
    private readonly AddPortfolio _addPortfolioControl = new AddPortfolio();
    private readonly CreatePortfolio _createPortfolioControl;
    private object _content;
    private ExpertClient Client { get; set; }

    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    public MainViewModel(ExpertClient client)
    {
        Client = client;
        Content = _portfolioControl;
        MainViewCommand = new RelayCommand((_) => Content = _portfolioControl);
        DiscoveryCommand = new RelayCommand((_) => Content = _discoveryViewControl);
        CreatePortfolioCommand = new RelayCommand((_) => Content = _createPortfolioControl);
        AddPortfolioCommand = new RelayCommand((_) => Content = _addPortfolioControl);
        Portfolios = client.Portfolios;
        
        _createPortfolioControl = new CreatePortfolio(client);
        _portfolioControl.Portfolios = Portfolios;
        
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}