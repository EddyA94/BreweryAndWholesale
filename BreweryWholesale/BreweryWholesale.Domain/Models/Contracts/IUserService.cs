using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Domain.Models.DTO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IUserService
    {
        Task<string> LoginUser(User_Dto user);
    }
}
