using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.Wrapper;

using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    public class FriendDetailViewModel : DetailViewModelBase, IFriendDetailViewModel
    {
        IFriendRepository _friendRepository;
        private FriendWrapper _friend;        
        private IMessageDialogService _messageDialogService;
        private IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;

        public FriendDetailViewModel(IFriendRepository friendRepository,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IProgrammingLanguageLookupDataService programmingLanguageLookupDataService
            ) : base(eventAggregator) 

        {
            _friendRepository = friendRepository;            
            _messageDialogService = messageDialogService;
            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

          

            AddPhoneNumberCommand = new DelegateCommand(OnAddPhoneNumberExecute);
            RemovePhoneNumberCommand = new DelegateCommand(OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);


            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            PhoneNumbers = new ObservableCollection<FriendPhoneNumberWrapper>();  




        }

        private bool OnRemovePhoneNumberCanExecute()
        {
            return SelectedPhoneNumber != null;
        }

        private void OnAddPhoneNumberExecute()
        {
            throw new NotImplementedException();
        }

        private void OnRemovePhoneNumberExecute()
        {
            throw new NotImplementedException();
        }

        protected override async void OnDeleteExecute()
        {
            if (_messageDialogService.ShowOkCancel($"Are you sure you want to delete {Friend.Model.FirstName} {Friend.Model.LastName}?", "Delete") == MessageDialogResult.OK)
            {
                _friendRepository.Remove(Friend.Model);
                await _friendRepository.SaveAsync();
                RaiseDetailDeletedEvent(Friend.Id);                 
            }
        }

     
        private FriendPhoneNumberWrapper _selectedPhoneNumber;

      
        protected override bool OnSaveCanExecute()
        {

            return Friend != null
                && !Friend.HasErrors
                && HasChanges
                && PhoneNumbers.All(ph => !ph.HasErrors); 
        }

        protected override  async void OnSaveExecute()
        {

            await _friendRepository.SaveAsync();

            HasChanges = _friendRepository.HasChanges();
            RaiseDetailSavedEvent(Friend.Id, Friend.FirstName + " " + Friend.LastName);             
        }



        public override async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue ?
                await _friendRepository.GetByIdAsync(friendId.Value) :
                CreateNewFriend();

            await InitializeFriend(friendId);
            await InitializeFriendPhoneNumber(friend.PhoneNumbers);

            await LoadProgrammingLanguages();
        }

        private async Task InitializeFriendPhoneNumber(ICollection<FriendPhoneNumber> phoneNumbers)
        {
            foreach (var phoneNumber in PhoneNumbers)
            {
                phoneNumber.PropertyChanged -= FriendPhoneNumberWrapper_PropertyChanged;
            }
            PhoneNumbers.Clear();
            foreach (var friendPhoneNumber in phoneNumbers)
            {
                var wrapper = new FriendPhoneNumberWrapper(friendPhoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += FriendPhoneNumberWrapper_PropertyChanged;
            }
        }

        private void FriendPhoneNumberWrapper_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _friendRepository.HasChanges();
            }
            if (e.PropertyName == nameof(FriendPhoneNumberWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private async Task InitializeFriend(int? friendId)
        {
            var friend = friendId.HasValue ?
                await _friendRepository.GetByIdAsync(friendId.Value) :
                CreateNewFriend();
            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _friendRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            // Little trick to  tigger the validation
            if (Friend.Id == 0)
            {
                Friend.FirstName = string.Empty;
            }
        }

        private async Task LoadProgrammingLanguages()
        {
            ProgrammingLanguages.Clear();
            ProgrammingLanguages.Add(new NullLookupItem { DisplayMember = "---" });
            var lookup = await _programmingLanguageLookupDataService.GetProgrammingLanguagesAsync();
            foreach (var language in lookup)
            {
                ProgrammingLanguages.Add(language);
            }
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            _friendRepository.Add(friend);
            return friend;

        }

        public FriendWrapper Friend
        {
            get
            {
                return _friend;
            }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }
        public FriendPhoneNumberWrapper SelectedPhoneNumber
        {
            get
            {
                return _selectedPhoneNumber;
            }
            set
            {
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
            }
        }
     
        public ICommand AddPhoneNumberCommand { get; }
        public ICommand RemovePhoneNumberCommand { get; set; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; set; }
        public ObservableCollection<FriendPhoneNumberWrapper> PhoneNumbers
        {
            get;
        }
    }
    }
