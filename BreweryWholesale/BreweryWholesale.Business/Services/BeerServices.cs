using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Infrastructure.Repository;

namespace BreweryWholesale.Infrastructure.Services
{
    public class BeerServices : IBeerService
    {
        private readonly IBeerRepository _beerRepository;

        public BeerServices(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            try
            {
                var result = await _beerRepository.GetAllBeersAsync();
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}