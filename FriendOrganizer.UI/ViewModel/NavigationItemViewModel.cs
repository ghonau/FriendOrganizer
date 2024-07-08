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
        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenFriendDetailViewCommand = new DelegateCommand(OnOpenFriendDetailView);
            
            _eventAggregator = eventAggregator;

        }

      

        private void OnOpenFriendDetailView()
        {
            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(Id);
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
        public ICommand OpenFriendDetailViewCommand
        {
            get;
        }
     
    }
    
}
