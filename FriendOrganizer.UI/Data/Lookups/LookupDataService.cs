﻿using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Lookups
{
    public class LookupDataService : 
        IFriendLookupDataService, 
        IProgrammingLanguageLookupDataService,
        IMeetingLookupDataService
    {
        Func<FriendOrganizerDbContext> _contextCreator;
        public LookupDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }


        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Friends.AsNoTracking()
                    .Select(f => new LookupItem { Id = f.Id, DisplayMember = f.FirstName + " " + f.LastName })
                    .ToListAsync();

            }
        }


        public async Task<IEnumerable<LookupItem>> GetProgrammingLanguagesAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.programmingLanguages.AsNoTracking()
                    .Select(f => new LookupItem { Id = f.Id, DisplayMember = f.Name })
                    .ToListAsync();

            }
        }
        public async Task<IEnumerable<LookupItem>> GetMeetingLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Meetings.AsNoTracking()
                    .Select(m => new LookupItem { Id = m.Id, DisplayMember = m.Title})
                    .ToListAsync();

            }
        }

    }
}
