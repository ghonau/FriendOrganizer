using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;



namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationViewModel navigationViewModel, IFriendDetailViewModel friendDetailViewModel)
        { 
            NavigationViewModel = navigationViewModel;
            FriendDetailViewModel = friendDetailViewModel; 

        }
        // main view model will take a dependency on the navigation and friend detail view model 
        public async Task LoadAsync()
        {
                 NavigationViewModel.LoadAsync(); 
        }

        private IFriendDetailViewModel _friendDetailViewModel;
        public IFriendDetailViewModel FriendDetailViewModel
        {
            get { return _friendDetailViewModel; }
            private set
            {
                _friendDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public INavigationViewModel NavigationViewModel { get; }
           
        
        
    }
}
