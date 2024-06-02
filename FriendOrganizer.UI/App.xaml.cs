using Autofac;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Startup;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Bootstrapper bootstrapper = new Bootstrapper();
            var container = bootstrapper.Build();
            var mainWindow = container.Resolve<MainWindow>();


            mainWindow.Show();
        }
    }

}
