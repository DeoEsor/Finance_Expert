using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DryIoc;
using Expert.DesktopApp.Annotations;
using Expert.DesktopApp.MVVM.Commands;
using Expert.DesktopApp.UserControls;
using Expert.gRPC.Client;
using Grpc.Core;
using Users.gRPC.Client;
using UserService.Messages;
using MessageBoxCustom = Expert.DesktopApp.UserControls.MessageBoxCustom;

namespace Expert.DesktopApp.MVVM.ViewModel;

public sealed class RegistrationViewModel : INotifyPropertyChanged
{
    public Window Owner { get; set; }
    public UserAuthorizationService UserAuthorizationService { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; set; }
    public string UserName { get; set; } = "Имя пользователя";
    public ICommand ChangeToConsumerCommand { get; set; }
    public ICommand ChangeToExpertCommand { get; set; }
    private UserStatus _userStatus = UserStatus.Consumer;
    public RegistrationViewModel(Window owner)
    {
        Owner = owner;
        UserAuthorizationService = App.Container.Resolve<UserAuthorizationService>();
        LoginCommand = new RelayCommand(Login);
        RegisterCommand = new RelayCommand(Register);
        ChangeToConsumerCommand = new RelayCommand((_) => _userStatus = UserStatus.Consumer);
        ChangeToExpertCommand = new RelayCommand((_) => _userStatus = UserStatus.Expert);
    }

    private async void Register(object obj)
    {
        if (obj is not PasswordBox passwordBox) return;
        var a =App.Container.Resolve<MainWindow>();
        var reply = UserAuthorizationService.Register(UserName, passwordBox.Password, _userStatus);
        
        if ((StatusCode)reply.StatusCode != StatusCode.OK)
        {
            bool? messageBox;
            switch ((StatusCode)reply.StatusCode)
            {
                case StatusCode.Aborted:
                    messageBox = new UserControls.MessageBoxCustom("Something with server, try later", MessageType.Error, MessageButtons.Ok)
                        .ShowDialog();
                    break;
                case StatusCode.AlreadyExists:
                    messageBox = new UserControls.MessageBoxCustom("This username is defined try another", MessageType.Error, MessageButtons.Ok)
                        .ShowDialog();
                    break;
            }
                
            return;
        }

        App.Container.Resolve<MainViewModel>().User = reply.User;
        
        a.Show();
        Owner.Close();
    }

    private async void Login(object obj)
    {
        if (obj is not PasswordBox passwordBox) return;
        var a = App.Container.Resolve<MainWindow>();
        var reply = UserAuthorizationService.Auth(UserName, passwordBox.Password, _userStatus);
        
        if ((StatusCode)reply.StatusCode != StatusCode.OK)
        {
            bool? messageBox;
            switch ((StatusCode)reply.StatusCode)
            {
                case StatusCode.Aborted:
                    messageBox = new UserControls.MessageBoxCustom("Something with server, try later", MessageType.Error, MessageButtons.Ok)
                        .ShowDialog();
                    break;
                case StatusCode.AlreadyExists:
                    messageBox = new UserControls.MessageBoxCustom("This username is defined try another", MessageType.Error, MessageButtons.Ok)
                        .ShowDialog();
                    break;
            }
                
            return;
        }
        
        App.Container.Resolve<MainViewModel>().User = reply.User;
        
        a.Show();
        Owner.Close();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}