﻿using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase,  INavigationViewModel
    {
        public ObservableCollection<LookupItem> Friends { get; }
        private IFriendLookupDataService _friendLookupDataService;
        private IEventAggregator _eventAggregator;

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService, IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<LookupItem>();
        }
        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(item);
            }
        }

        private LookupItem _selectedFriend; 
        public LookupItem SelectedFriend {
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
                    _eventAggregator.GetEvent<OpenFriendDetaialViewEvent>().Publish(_selectedFriend.Id); 
                }
            }
        }

    }
}
