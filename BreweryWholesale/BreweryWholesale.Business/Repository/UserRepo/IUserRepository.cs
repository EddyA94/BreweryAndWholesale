using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository.UserRepo
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserName(string userName);
    }
}
