using FriendOrganizer.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.DataAccess.Specifications
{
    public class GetFriendByFirstNameSpecification : Specification<Friend>
    {
        // This spec class will recieve parameter
        // This concrete spec class will pass the parameters to the base specific class
        // And will create a criteria as the base class create criteria
        public GetFriendByFirstNameSpecification(string  firstName) : base( name => name.FirstName == firstName) 
        {
        
        }
    }
}
