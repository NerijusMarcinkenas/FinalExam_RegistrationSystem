using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Core.Interfaces;
using RegistrationSystem.Core.Models;
using RegistrationSystem.DAL.Data;

namespace RegistrationSystem.DAL.Repositories
{

    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _db;

        public UserRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }
        public async Task<bool> IsUserExists(string username) =>
            await _db.Users.AnyAsync(u => u.Username == username);

        public async Task<User?> GetUserByUsername(string username) =>
            await _db.Users.SingleOrDefaultAsync(u => u.Username == username);

        public async Task<User?> GetUserWithPerson(string userId)
        {
            return await _db.Users
                .Include(p => p.Person)
                .SingleOrDefaultAsync(i => i.Id == userId);
        }

    }
}
