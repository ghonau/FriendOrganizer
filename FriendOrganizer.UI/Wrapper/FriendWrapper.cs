﻿using FriendOrganizer.Model;
using FriendOrganizer.UI.ViewModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Wrapper
{
    public class FriendWrapper : ViewModelBase, INotifyDataErrorInfo 
    {
        public Friend Model { get; set; }
        public FriendWrapper(Friend friend)
        {
            Model = friend; 
        }
        public int Id { get { return Model.Id; } }
        public string FirstName
        {
            get
            {
                return Model.FirstName; 
            }
            set
            {
                Model.FirstName = value;
                OnPropertyChanged(); // This is the method from the ViewModelBase class
                ValidateProperty(nameof(FirstName));
            }
        }

        private void ValidateProperty(string propertyName)
        {
            ClearErrors(propertyName);
            switch (propertyName)
            {
                case nameof(FirstName):
                    {
                        if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                        {
                            AddError(propertyName, "Robots are not valid friends");
                        }
                        break;
                    }
            }
        }
        // The funny thing is that INotifyDataERrorInfo will set the
        // // validation property of the Textbox for us
        // Because INotifyDataErrorInfo will set the validation property of the textbox for us
        // It will change the style of the border as well once the errors occured.

        public string LastName
        {
            get
            {
                return Model.LastName;
            }
            set
            {
                Model.LastName = value;
                OnPropertyChanged(); // This is the method from the ViewModelBase class
            }
        }
        public string Email
        {
            get
            {
                return Model.Email; 

            }
            set
            {
                Model.Email = value;
                OnPropertyChanged(); // This is the method from the ViewModelBase class
                // View model base class will raise the PropertyChanged Event wtih this mehtod 

            }
        }
        private Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ?
                _errorsByPropertyName[propertyName] :
                null;
        }
        

        // OnErrorsChanged is our method to raise the ErrorsChanged event.
        private void OnErrorsChanged(string propertyName)
        {

           ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void AddError(string propertyName, string error)
        {
           if(!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            if(!_errorsByPropertyName[propertyName].Contains(error)) // Check to see if the 
                // List already contains the error.
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
                // This is a method call which will raise the ErrorsChanged event. 
            }
        }
        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName); 
            }
        }
    }
}