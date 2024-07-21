using FriendOrganizer.UI.Event;

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
    public class NavigationItemViewModel : ViewModelBase
    {
        private string _displayMember; 
        private IEventAggregator  _eventAggregator;
        public NavigationItemViewModel(int id,
            string displayMember, 
            IEventAggregator eventAggregator,
            string detailViewModelName
            )
        {
            Id = id;
            DisplayMember = displayMember;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
            _detailViewModelName = detailViewModelName;
            
            _eventAggregator = eventAggregator;

        }

      

        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(new OpenDetailViewEventArg
            {
                Id = Id , 
                ViewModelName = _detailViewModelName
            });
        }

        public int Id { get; set; }
        public string DisplayMember
        {
            get
            {
                return _displayMember; 
            }
            set
            {
                _displayMember = value;
                OnPropertyChanged();

            }
        }
        public ICommand OpenDetailViewCommand
        {
            get;
        }

        private string _detailViewModelName;
    }
    
}
