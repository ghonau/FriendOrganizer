using FriendOrganizer.Model;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Event;

using Prism.Events;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using FriendOrganizer.UI.Event;
using System.Windows;
using FriendOrganizer.UI.View.Services;
using Prism.Commands;
using System.Security.Permissions;
using System.Windows.Input;



namespace FriendOrganizer.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        IMessageDialogService _messageDialogService; 
        public MainViewModel(INavigationViewModel navigationViewModel,
            // With this Func  you can create as many as you want
            Func<IFriendDetailViewModel> friendDetailViewModelCreator,
            IMessageDialogService messageDialogService,
            IEventAggregator eventAggregator)
        { 
            
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Subscribe(OnOpenFriendDetailView);
            _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Subscribe(OnAfterFriendDeleted); 

            CreateNewFriendCommand = new DelegateCommand(OnCreateNewFriendExecute);

            NavigationViewModel = navigationViewModel;

        }

        private void OnAfterFriendDeleted(int friendId)
        {
            FriendDetailViewModel = null; 
        }

        private void OnCreateNewFriendExecute()
        {
            OnOpenFriendDetailView(null);
        }

        // main view model will take a dependency on the navigation and friend detail view model 
        public async Task LoadAsync()
        {
               await  NavigationViewModel.LoadAsync(); 
        }

        private IFriendDetailViewModel _friendDetailViewModel;
        
        public IFriendDetailViewModel FriendDetailViewModel
        {
            get
            {
                 return _friendDetailViewModel;
            }
            private set
            {
                _friendDetailViewModel = value;
                OnPropertyChanged(); // Raise the property changed events 
            }
        }

        private IEventAggregator _eventAggregator;

        public INavigationViewModel NavigationViewModel { get; }

        public ICommand CreateNewFriendCommand { get; }
        

        private Func<IFriendDetailViewModel> _friendDetailViewModelCreator;



        private async void OnOpenFriendDetailView(int? friendId)
        {
            if (FriendDetailViewModel != null && FriendDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancel("You have made some changes. Navigate away?", "Question");
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }

            }
            FriendDetailViewModel = _friendDetailViewModelCreator();
            await FriendDetailViewModel.LoadAsync(friendId);
            
            // We are creating a new FriendDetailViewModel per a friend selected on navigation
            // Which will create a brand repository and branch new dbcontext
            
        }
        

        



    }
}
