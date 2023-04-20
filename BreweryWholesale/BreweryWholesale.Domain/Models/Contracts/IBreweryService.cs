using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Domain.Models.DTO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IBreweryService
    {
        Task<Brewery> GetBeersByBreweryName(string BreweryName);
    }
}
