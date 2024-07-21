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
        public ObservableCollection<NavigationItemViewModel> Meetings { get; }
        private IFriendLookupDataService _friendLookupDataService;
        private IMeetingLookupDataService _meetingLookupDataService;
        private IEventAggregator _eventAggregator;

        public NavigationViewModel(IFriendLookupDataService friendLookupDataService,
            IMeetingLookupDataService meetingLookupDataService,
            IEventAggregator eventAggregator)
        {
            _friendLookupDataService = friendLookupDataService;
            _meetingLookupDataService = meetingLookupDataService; 
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            Meetings = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeletedEvent); 
        }

        

    

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupDataService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator
                    ,nameof(FriendDetailViewModel)));
              
            }
            lookup = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meetings.Clear();
            foreach (var item in lookup)
            {
                Meetings.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator
                    , nameof(MeetingDetailViewModel)));

            }
        }

        private void AfterDetailDeletedEvent(AfterDetailDeletedEventArgs afterDetailDeletedEventArgs)
        {
            switch (afterDetailDeletedEventArgs.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    {
                        var friend = Friends.SingleOrDefault(f => f.Id ==  afterDetailDeletedEventArgs.Id);
                        if (friend != null) 
                        {
                            Friends.Remove(friend); 
                        }
                        break;
                    }
                case nameof(MeetingDetailViewModel):
                    {
                        var meeting = Meetings.SingleOrDefault(f => f.Id == afterDetailDeletedEventArgs.Id);
                        if (meeting != null)
                        {
                            Meetings.Remove(meeting);
                        }
                        break;
                    }
            }
        }
        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(FriendDetailViewModel):
                    {
                        CreateNavigationItemAfterDetailSaved(args, Friends);
                        break;
                    }
                case nameof(MeetingDetailViewModel):
                    {

                        CreateNavigationItemAfterDetailSaved(args, Meetings); 
                        break;
                    }
            }
        }

        private void CreateNavigationItemAfterDetailSaved(AfterDetailSavedEventArgs args, ObservableCollection<NavigationItemViewModel> items)           
        {
            var lookupItem = items.SingleOrDefault(f => f.Id == args.Id);
            if (lookupItem != null)
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
            else
            {
                Friends.Add(new NavigationItemViewModel(args.Id,
                    args.DisplayMember, _eventAggregator, args.ViewModelName));
            }
        }
    }
}
