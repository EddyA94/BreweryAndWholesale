using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IBeerService
    {
        Task<IEnumerable<Beer>> GetAllBeersAsync();
    }
}
