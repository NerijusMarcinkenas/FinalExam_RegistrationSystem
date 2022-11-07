using Microsoft.EntityFrameworkCore;
using RegistrationSystem.Core.Models;
using RegistrationSystem.DAL.Data;

namespace RegistrationSystem.DAL.Repositories
{
    internal class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly ApplicationContext _dbContext;

        public PersonRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
                
        public async Task<Person?> GetPersonByUserId(string userId)
        {
            return await _dbContext.People.SingleOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<Person?> GetPersonWithIncludesAsync(string userId)
        {
            if (_dbContext.People.SingleOrDefault(u => u.UserId == userId) is null)
            {
                return null;
            }

            return await _dbContext.People
                .Include(a => a.Address)
                .Include(i => i.Image)
                .SingleOrDefaultAsync(i => i.UserId == userId);
        }
    }
}
