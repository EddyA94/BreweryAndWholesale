using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Infrastructure.Exceptions;
using BreweryWholesale.Infrastructure.Repository;

namespace BreweryWholesale.Infrastructure.Services
{
    public class BreweryServices : IBreweryService
    {
        private readonly IBreweryRepository _breweryRepository;
        public BreweryServices(IBreweryRepository BreweryRepository)
        {
            _breweryRepository = BreweryRepository;
        }

        public async Task<Brewery> GetAllBeersByBreweryNameAsync(string breweryName)
        {
            try
            {
                if (breweryName == string.Empty) return default;
                var result = await _breweryRepository.GetAllBeersByBreweryNameAsync(breweryName);              
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}