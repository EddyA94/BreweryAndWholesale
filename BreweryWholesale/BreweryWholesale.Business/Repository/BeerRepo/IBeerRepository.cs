using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository.BeerRepo
{
    public interface IBeerRepository
    {
        Task<IEnumerable<Beer>> GetAllBeersAsync();
        Task<IEnumerable<Beer>> GetBeersByBreweryNameAndBeerNameAsync(int breweryID, string beerName);
        Task<IEnumerable<Beer>> GetBeersByIdsAsync(IEnumerable<int> beerIds);
        Task AddBeerAsync(Beer beer);
        Task DeleteBeerAsync(Beer beer);
    }

}
