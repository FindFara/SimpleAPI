using SimpleAPI.Data.Entities;

namespace SimpleAPI.Data.Repositories
{
    public interface IUserRepository
    {
    
        IEnumerable<User> GetAll();
        Task<User> Add(User User);
        Task<User> Find(int id);
        Task<User> Update(User User);
        Task<User> Remove(int id);
        Task<bool> IsExists(int id);

    }
}
