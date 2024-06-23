using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data
{
    public interface IFriendDataService
    {
       Task<List<Friend>> GetAllAsync();
        Task<Friend> GetByIdAsync(int friendId); 

        Task SaveFriend(Friend friend);
    }
}