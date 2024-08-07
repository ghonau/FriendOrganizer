﻿using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data.Repositories
{
    public interface IMeetingRepository : IGenericRepository<Meeting>
    {
        Task<Meeting> GetByIdAsync(int id);
    }
}