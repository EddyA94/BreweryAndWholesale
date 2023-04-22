using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IBreweryService
    {
        Task<Brewery> GetAllBeersByBreweryNameAsync(string BreweryName);
    }
}
