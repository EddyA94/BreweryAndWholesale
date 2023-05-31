using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository.BreweryRepo
{
    public interface IBreweryRepository
    {
        Task<Brewery?> GetAllBeersByBreweryIdAsync(int breweryId);
        Task<Brewery?> GetAllBeersByBreweryNameAsync(string breweryName);
        Task<Brewery> GetBreweryByNameAsync(string breweryName);
    }

}
