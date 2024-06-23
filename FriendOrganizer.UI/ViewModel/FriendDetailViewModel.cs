using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.Wrapper;

using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        IFriendDataService _friendDataService;
        IEventAggregator _eventAggregator;
        public FriendDetailViewModel(IFriendDataService friendDataService,
            IEventAggregator eventAggregator)
        {
            _friendDataService = friendDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);


        }

        private bool OnSaveCanExecute()
        {
            return true; 
        }

        private async void OnSaveExecute()
        {
            
            await _friendDataService.SaveFriend(Friend.Model);

            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(new AfterFriendSavedEventArgs
            {
                DisplayMember = Friend.FirstName + " " + Friend.LastName,
                Id = Friend.Id
            }); 
        }

        private async void OnOpenFriendDetailView(int friendId)
        {
            await LoadAsync(friendId);
        }

        public async Task LoadAsync(int friendId)
        {
            var friend = await _friendDataService.GetByIdAsync(friendId);
            Friend = new FriendWrapper(friend);
        }

        private FriendWrapper _friend;
        public FriendWrapper Friend
        {
            get
            {
                return _friend;
            }
            private set
            {
                _friend = value;
                OnPropertyChanged(nameof(Friend));
            }
        }

        public ICommand SaveCommand { get; }

    }
}
