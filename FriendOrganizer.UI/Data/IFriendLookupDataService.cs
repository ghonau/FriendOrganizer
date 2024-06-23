using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data
{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<NavigationItemViewModel>> GetFriendLookupAsync();

        
    }
}