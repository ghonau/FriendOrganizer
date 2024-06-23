﻿using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using FriendOrganizer.Model; 

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
        }

        private void AfterFriendSaved(AfterFriendSavedEventArgs args)
        {
            var lookupItem = Friends.Single(f => f.Id == args.Id);            
            lookupItem.DisplayMember = args.DisplayMember; 
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
              
            }
        }

        private NavigationItemViewModel _selectedFriend; 
        public NavigationItemViewModel SelectedFriend {
            get
            {
                return _selectedFriend; 
            }
            set
            {
                _selectedFriend = value;
                OnPropertyChanged(); 
                if(_selectedFriend != null )
                {
                    _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(_selectedFriend.Id); 
                }
            }
        }

    }
}
