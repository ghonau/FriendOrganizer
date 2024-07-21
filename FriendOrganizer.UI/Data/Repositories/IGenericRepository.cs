using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task SaveAsync();
        bool HasChanges();
        void Add(T frined);
        void Remove(T model);

    }
}