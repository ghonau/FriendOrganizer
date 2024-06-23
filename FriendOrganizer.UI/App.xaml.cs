using Autofac;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Startup;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls.Ribbon.Primitives;

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

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Uxpected error occured. Please inform the admin." + Environment.NewLine + e.Exception.Message, "Unexpected error");
            e.Handled = true; 
            
           // We do not want to pulote the mdodel so we wilil be not handlding the INotifyDataErrorInfo in the 
           // Model itself 
           // We do not want to have them in the view model either as it is meant to deal with the view logic
           // We will handle the INotifyDataErrorInfo in the model wrapper class

            

            
        }
    }

}
