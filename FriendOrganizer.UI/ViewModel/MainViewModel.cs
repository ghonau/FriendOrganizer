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
        
        
        
        
      

        public INavigationViewModel        NavigationViewModel { get; set; }
        public IFriendDetailViewModel FriendDetailViewModel { get; }

        public MainViewModel(INavigationViewModel navigationViewModel, IFriendDetailViewModel friendDetailViewModel )
        {
                NavigationViewModel = navigationViewModel;        
            FriendDetailViewModel = friendDetailViewModel;
        }

        
        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
      

           
        
        
    }
}
