using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data.Lookups
{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();

        Task<IEnumerable<LookupItem>> GetProgrammingLanguagesAsync(); 
    }
}