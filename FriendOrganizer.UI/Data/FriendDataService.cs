using FriendOrganizer.DataAccess;
using FriendOrganizer.DataAccess.Specifications;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private Func<FriendOrganizerDbContext> _contextCreator; 
        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;   
        }
        public   async Task<List<Friend>> GetAllAsync()
        {
            
            using( var ctx = _contextCreator())
            {                
                
                return  await ctx.Friends.AsNoTracking().ToListAsync();                               
            }
            

        }
        public async   Task<Friend> GetByIdAsync(int friendId)
        {
            using( var ctx = _contextCreator())
            {
                return await ctx.Friends.AsNoTracking().SingleAsync(f => f.Id  == friendId);
            }
        }

        public async Task SaveFriend(Friend friend)
        {
            using(var ctx = _contextCreator())
            {
                ctx.Friends.Attach(friend);
                ctx.Entry(friend).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
        public async Task<Friend> GetByFirstName(string firstName)
        {
            using (var ctx = _contextCreator())
            {
              return await  ApplySpecification(ctx.Friends, new GetFriendByFirstNameSpecification(firstName)).FirstOrDefaultAsync();  
            }
        }

        private IQueryable<Friend> ApplySpecification(IQueryable<Friend> input,  Specification<Friend> specification) 
        {
            return SpecificationEvaluator.GetQuery(input, specification);
        }
    }
}
