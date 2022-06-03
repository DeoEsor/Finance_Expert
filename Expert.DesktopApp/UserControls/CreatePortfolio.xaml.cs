using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Expert.DesktopApp.MVVM.Commands;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Expert.DesktopApp.UserControls;

public partial class CreatePortfolio : UserControl
{
    public ICommand SendCommand { get; set; }
    public ObservableCollection<CheckBox> Stocks { get; set; } = new ObservableCollection<CheckBox>()
    {
        new CheckBox{ Content = "IBM", Foreground = Brushes.White},
        new CheckBox{ Content = "AAPL", Foreground = Brushes.White},
        new CheckBox{ Content = "HPQ", Foreground = Brushes.White},
        new CheckBox{ Content = "MSFT", Foreground = Brushes.White},
        new CheckBox{ Content = "T", Foreground = Brushes.White}
    };
    public CreatePortfolio(gRPC.Client.ExpertClient client)
    {
        InitializeComponent();
        SendCommand = new RelayCommand(_ => client
            .CreatePortfolioByStocks(Stocks.Where(s => s.IsChecked != null &&  s.IsChecked.Value)
                .Select(s => Content.ToString())));
    }
}