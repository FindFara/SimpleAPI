using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SimpleAPI.Data.Context;
using SimpleAPI.Data.Entities;

namespace SimpleAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SimpleAPIDbContext _context;
        private IMemoryCache _cache;

        public UserRepository()
        {

        }
        public UserRepository(SimpleAPIDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }


        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Find(int id)
        {
            var cacheUser = _cache.Get<User>(id);
            if (cacheUser != null)
            {
                return cacheUser;
            }
            else
            {
                var User = await _context.Users.SingleOrDefaultAsync(c => c.Id == id);
                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(User.Id, User, cacheOption);
                return User;

            }

        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Users.AnyAsync(c => c.Id == id);
        }

        public async Task<User> Remove(int id)
        {
            var User = await _context.Users.SingleAsync(c => c.Id == id);
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User> Update(User User)
        {
            _context.Update(User);
            await _context.SaveChangesAsync();
            return User;
        }
    }
}
