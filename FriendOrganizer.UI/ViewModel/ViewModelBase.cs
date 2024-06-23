using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace FriendOrganizer.UI.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        // This is just a method in the base class we are uing this class
        // in the inheriting classes to raise the property changed event.
        // The inheriting class will have a property or member which will call this method.
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // This is the method which will be called by the inheriting class 
            // To raise the property change error. 


        }
    }
}   