using BreweryWholesale.Domain.Models.DBO;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Infrastructure.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly BrewerWholesaleDBContext _context;

        public UserRepository(BrewerWholesaleDBContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
           return await _context.Set<User>().Where(W=>W.UserName == userName).FirstOrDefaultAsync();
        }
    }
}
