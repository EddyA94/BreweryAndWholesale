using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository
{
    public interface IBreweryRepository
    {
        Task<Brewery?> GetAllBeersByBreweryNameAsync(string breweryName);
        Task<Brewery> GetBreweryByNameAsync(string breweryName);
    }

}
