using Microsoft.EntityFrameworkCore;
using TestProjectAzeriMed.Application.Contracts.Identity;
using TestProjectAzeriMed.Application.Models.Identity;
using TestProjectAzeriMed.Identity;
using TestProjectAzeriMed.Identity.Models;

namespace SpendLess.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly TestProjectIdentityDbContext _dbContext;

        public UserService(TestProjectIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _dbContext.Set<ApplicationUser>().FirstOrDefaultAsync(x => x.ID == userId);
            return new User
            {
                Name = user.Name,
                DateOfBirth = user.DateOfBirth
            };
        }

        public async Task<List<User>> GetUsers()
        {
            var users = await _dbContext.Set<ApplicationUser>().ToListAsync();
            return users.Select(q => new User
            {
                Name = q.Name,
                DateOfBirth = q.DateOfBirth
            }).ToList();
        }
    }
}
