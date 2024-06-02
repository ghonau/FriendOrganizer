using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModel;

namespace FriendOrganizer.UI.Startup
{
    public class Bootstrapper
    {
        public Autofac.IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();
            
            
            builder.RegisterType<MainViewModel>().AsSelf(); 
            builder.RegisterType<MainWindow>().AsSelf(); 
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();
            builder.RegisterType<FriendLookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            return builder.Build(); 

        }
    }
}
