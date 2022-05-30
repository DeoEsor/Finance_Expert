using System.Windows.Controls;
using Expert.DesktopApp.Core.Interfaces;

namespace Expert.DesktopApp;

public partial class BindablePasswordBox : UserControl, IPasswordSupplier
{
    public BindablePasswordBox()
    {
        InitializeComponent();
    }
    public string GetPassword()
    {
        return pwdBox.Password;
    }
}