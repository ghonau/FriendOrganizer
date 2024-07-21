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

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(OnAfterDetailDeleted); 

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewExecute);

            NavigationViewModel = navigationViewModel;

        }

        private void OnAfterDetailDeleted(AfterDetailDeletedEventArgs afterDetailDeletedEventArgs)
        {
            DetailViewModel = null; 
        }

        private void OnCreateNewExecute(Type viewModelType)
        {
            OnOpenDetailView(  new OpenDetailViewEventArg
            {
                ViewModelName = viewModelType.Name
            });
        }

        // main view model will take a dependency on the navigation and friend detail view model 
        public async Task LoadAsync()
        {
               await  NavigationViewModel.LoadAsync(); 
        }

        private IDetailViewModel _detailViewModel;
        
        public IDetailViewModel DetailViewModel
        {
            get
            {
                 return _detailViewModel;
            }
            private set
            {
                _detailViewModel = value;
                OnPropertyChanged(); // Raise the property changed events 
            }
        }

        private IEventAggregator _eventAggregator;

        public INavigationViewModel NavigationViewModel { get; }

        public ICommand CreateNewDetailCommand { get; }
        

        private Func<IDetailViewModel> _friendDetailViewModelCreator;



        private async void OnOpenDetailView(OpenDetailViewEventArg openDetailViewEventArg)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancel("You have made some changes. Navigate away?", "Question");
                if(result == MessageDialogResult.Cancel)
                {
                    return;
                }

            }
            switch (openDetailViewEventArg.ViewModelName)
            {
                case nameof(FriendDetailViewModel):

                    {
                        DetailViewModel = _friendDetailViewModelCreator();
                        break;

                    }
            }
            await DetailViewModel.LoadAsync(openDetailViewEventArg.Id);
            
            // We are creating a new FriendDetailViewModel per a friend selected on navigation
            // Which will create a brand repository and branch new dbcontext
            
        }
        

        



    }
}
