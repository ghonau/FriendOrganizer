using FriendOrganizer.Model;
using FriendOrganizer.UI.ViewModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Wrapper
{
    public class FriendWrapper : ModelWrapper<Friend>  
    {
        
        public FriendWrapper(Friend friend) : base(friend) { }
        
        public int Id { get { return Model.Id; } }
        public string FirstName
        {
            get
            {
                return GetValue<string> ();    
            }
            set
            {
                SetValue(value);                
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
                return GetValue<string>();
            }
            set
            {
                 SetValue(value); 
            }
        }
        public string Email
        {
            get
            {
                return GetValue<string>();

            }
            set
            {
                SetValue(value);

            }
        }
        public int? FavoriteLanguageId
        {
            get
            {
                return  GetValue<int?> (); 
            }
            set
            {
                SetValue(value);    
            }
        }
        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            
            switch (propertyName)
            {
                case nameof(FirstName):
                    {
                        if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                        {
                            yield return   "Robots are not valid friends";
                        }
                        break;
                    }
            }
        }


    }
}
