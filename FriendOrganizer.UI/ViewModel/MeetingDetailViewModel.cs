using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.View.Services;
using FriendOrganizer.UI.Wrapper;

using Prism.Commands;
using Prism.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public interface IMeetingDetailViewModel : IDetailViewModel { }
    public class MeetingDetailViewModel : DetailViewModelBase,  IMeetingDetailViewModel
    {
        
        private IMeetingRepository _meetingRepository;
        private MeetingWrapper _meeting; 
        private IMessageDialogService _messageDialogService;
        public MeetingDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IMeetingRepository meetingRepository):base(eventAggregator)
        {
            _meetingRepository = meetingRepository;
            _messageDialogService = messageDialogService;
        }
        public bool HasChanges { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MeetingWrapper Meeting { get; private set; }

        public override async Task LoadAsync(int? id)
        {
            var meeting = id.HasValue
                ? await _meetingRepository.GetByIdAsync(id.Value) :
                CreateNewMeeting();
            InitializeMeeting(meeting); 
        }

     
        protected override void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancel($"Do you really want to delete the meeting {Meeting.Title}", "Delete Metting");
            if (result == MessageDialogResult.OK) 
            {
                _meetingRepository.Remove(Meeting.Model);
                _meetingRepository.SaveAsync();
                RaiseDetailDeletedEvent(Meeting.Id); 
            }
        }

        private Meeting CreateNewMeeting()
        {
            var meeting = new Meeting()
            {
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date,
            };
            _meetingRepository.Add(meeting);
            return meeting; 
        }
        private void InitializeMeeting(Meeting meeting)
        {
            Meeting = new MeetingWrapper(meeting);
            Meeting.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _meetingRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Meeting.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged(); 

        }

        protected override bool OnSaveCanExecute()
        {
            return Meeting != null && !Meeting.HasErrors && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _meetingRepository.SaveAsync();
            HasChanges = _meetingRepository.HasChanges();
            RaiseDetailSavedEvent(Meeting.Id, Meeting.Title); 
        }

    }
}
