﻿using FriendOrganizer.DataAccess;
using FriendOrganizer.DataAccess.Specifications;
using FriendOrganizer.Model;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class FriendRepository : GenericRepository<Friend, FriendOrganizerDbContext>, IFriendRepository
    {
        
        public FriendRepository(FriendOrganizerDbContext  context) : base(context)
        {
            
        }    

        
        public override async Task<Friend> GetByIdAsync(int friendId)
        {
            return await Context.Friends.Include(r=>r.PhoneNumbers).SingleAsync(f => f.Id == friendId);
        }          

        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            Context.FriendPhoneNumbers.Remove(model);
        }

       
       
    }
}
