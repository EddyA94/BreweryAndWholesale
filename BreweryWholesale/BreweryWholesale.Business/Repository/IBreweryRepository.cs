using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository
{
    public interface IBreweryRepository
    {
        Task<Brewery> GetAllBeersByBreweryName(string breweryName);
        Task<Brewery> GetBreweryByNameAsync(string breweryName);
    }

}
