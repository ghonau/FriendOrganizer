using FriendOrganizer.DataAccess;
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
    public class FriendRepository : IFriendRepository
    {
        private FriendOrganizerDbContext _context;
        public FriendRepository(FriendOrganizerDbContext  context)
        {
            _context = context;
        }

        public void Add(Friend frined)
        {
            _context.Friends.Add(frined);            
        }      

        public async Task<List<Friend>> GetAllAsync()
        {           
            return await _context.Friends.ToListAsync();        
        }
        public async Task<Friend> GetByIdAsync(int friendId)
        {
            
            
                
            // We will be tracking the context changes in the db contex so we are removing NoTracking
            // return await _context.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
            return await  _context.Friends.SingleAsync(f => f.Id == friendId);

        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Friend model)
        {
           _context.Friends.Remove(model);
            
        }

        public async Task SaveAsync()
        {                                       
           //db context already knows about the object
            await _context.SaveChangesAsync();            
        }
       
    }
}
