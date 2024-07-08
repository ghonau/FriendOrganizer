using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data.Repositories
{
    public interface IFriendRepository
    {
        Task<List<Friend>> GetAllAsync();
        Task<Friend> GetByIdAsync(int friendId);

        Task SaveAsync();
        bool HasChanges();
        void Add(Friend frined);
        void Remove(Friend model);
    }
}