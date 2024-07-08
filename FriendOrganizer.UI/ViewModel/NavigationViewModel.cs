using FriendOrganizer.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Lookups;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase,  INavigationViewModel
    {
        public ObservableCollection<NavigationItemViewModel> Friends { get; }
        private IFriendLookupDataService _friendLookupDataService;
        private IEventAggregator _eventAggregator;

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Subscribe(AfterFriendSaved);
            _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Subscribe(AfterFriendDeletedEvent); 
        }

        

    

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
              
            }
        }

        private void AfterFriendDeletedEvent(int friendId)
        {
            var friend = Friends.SingleOrDefault(f => f.Id == friendId);
            if (friend != null) 
            {
                Friends.Remove(friend); 
            }
        }
        private void AfterFriendSaved(AfterFriendSavedEventArgs args)
        {
            var lookupItem = Friends.SingleOrDefault(f => f.Id == args.Id);
            if (lookupItem != null)
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
            else
            {
                Friends.Add(new NavigationItemViewModel(args.Id, args.DisplayMember, _eventAggregator));
            }
        }
        //private NavigationItemViewModel _selectedFriend; 
        //public NavigationItemViewModel SelectedFriend {
        //    get
        //    {
        //        return _selectedFriend; 
        //    }
        //    set
        //    {
        //        _selectedFriend = value;
        //        OnPropertyChanged(); 
        //        if(_selectedFriend != null )
        //        {
        //            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(_selectedFriend.Id); 
        //        }
        //    }
        //}

    }
}
