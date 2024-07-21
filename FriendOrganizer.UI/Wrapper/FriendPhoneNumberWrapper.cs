using FriendOrganizer.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Wrapper
{
    public class FriendPhoneNumberWrapper : ModelWrapper<FriendPhoneNumber>
    {
        public FriendPhoneNumberWrapper(FriendPhoneNumber friendPhoneNumber) :base(friendPhoneNumber)
        { 
          
        }
        public string Number { 

            get
            {
                return GetValue<string>(); 
            }
            set 
            { 
                SetValue(value); 
            }
        }
    }
}
