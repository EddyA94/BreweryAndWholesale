using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;
using BreweryWholesale.Infrastructure.Repository.BeerRepo;

namespace BreweryWholesale.Infrastructure.Services
{
    public class BeerServices : IBeerService
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IBreweryService _breweryService;

        public BeerServices(IBeerRepository beerRepository, IBreweryService breweryService)
        {
            _beerRepository = beerRepository;
            _breweryService = breweryService;
        }

        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            try
            {
                var result = await _beerRepository.GetAllBeersAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddBeerAsync(Beer_Dto beer_Dto)
        {
            try
            {
                if (beer_Dto.BreweryId == 0)
                {
                    throw new CustomExceptions("Brewery Name Cannot be Empty", (int)System.Net.HttpStatusCode.BadRequest);
                }
                
                var brewery = await _breweryService.GetAllBeersByBreweryIdAsync(beer_Dto.BreweryId) ?? throw new CustomExceptions("Brewery Does not Exists", (int)System.Net.HttpStatusCode.NotFound);

                if (brewery.Beers?.Where(W => W.Name == beer_Dto.BeerName).Count() > 0)
                {
                    throw new CustomExceptions("Beer already Exists For Brewery", (int)System.Net.HttpStatusCode.Conflict);
                }
                Beer newBeer = new()
                {
                    Name = beer_Dto.BeerName,
                    BreweryID = brewery.BrewerID,
                    AlcoholContent = beer_Dto.AlcoholContent,
                    Price = beer_Dto.Price
                };
                await _beerRepository.AddBeerAsync(newBeer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteBeerAsync(int beerId)
        {
            try
            {
                var existingBeer = GetBeerByIdsAsync(new List<int> { beerId }).Result;
                if (existingBeer != null && existingBeer.FirstOrDefault() != null)
                {
                    await _beerRepository.DeleteBeerAsync(existingBeer.First());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Beer>> GetBeerByIdsAsync(IEnumerable<int> beerId)
        {
            var result = await _beerRepository.GetBeersByIdsAsync(beerId);
            return result ?? throw new CustomExceptions("Beer Does not exist", (int)System.Net.HttpStatusCode.NotFound);
        }
    }
}