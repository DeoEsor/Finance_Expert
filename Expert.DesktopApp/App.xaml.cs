using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DryIoc;
using Expert.DesktopApp.MVVM.ViewModel;
using Expert.gRPC.Client;
using Users.gRPC.Client;

namespace Expert.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
			
            Container = new Container();
			
            // Grpc client
            Container.Register<ExpertClient>(Reuse.Singleton);
            Container.Register<UserAuthorizationService>(Reuse.Singleton);
            //Views
            Container.Register<Registration>(Reuse.Transient);
            Container.Register<MainWindow>(Reuse.Transient);
            //ViewModels
            //Container.Register<ChatViewModel>(Reuse.Singleton);
			
            var reg = Container.Resolve<MainWindow>();
            //reg.DataContext = new RegistrationViewModel(reg);
            reg.Show();
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
        }
    }
}