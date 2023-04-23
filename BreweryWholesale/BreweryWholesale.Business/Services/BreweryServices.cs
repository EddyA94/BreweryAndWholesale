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
                var result = await _breweryRepository.GetAllBeersByBreweryNameAsync(breweryName);              
                return result ?? throw new CustomExceptions("Brewery Does not Exists", (int)System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}