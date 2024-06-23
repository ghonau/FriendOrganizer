using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace FriendOrganizer.UI.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // This is our ViewModelBase class which implements INotifyPropertyChanged.


        }
    }
}   