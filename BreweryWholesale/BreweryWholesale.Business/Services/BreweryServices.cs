using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Repository;

namespace BreweryWholesale.Infrastructure.Services
{
    public class BreweryServices : IBreweryService
    {
        private readonly IBreweryRepository _breweryRepository;
        private readonly IBeerService _beerService;
        public BreweryServices(IBreweryRepository BreweryRepository, IBeerService beerService)
        {
            _breweryRepository = BreweryRepository;
            _beerService = beerService;
        }

        public async Task<Brewery> GetBeersByBreweryName(string breweryName)
        {
            try
            {
                if (breweryName == string.Empty) return default;
                var result = await _breweryRepository.GetAllBeersByBreweryNameAsync(breweryName);
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}