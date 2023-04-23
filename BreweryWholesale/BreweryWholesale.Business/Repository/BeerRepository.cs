using BreweryWholesale.Domain.Models.DBO;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Infrastructure.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BrewerWholesaleDBContext _context;

        public BeerRepository(BrewerWholesaleDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            return await _context.Set<Beer>().ToListAsync();
        }

        public async Task<IEnumerable<Beer>> GetBeersByBreweryNameAndBeerNameAsync(int breweryID, string beerName)
        {
            return await _context.Set<Beer>().Where(W => W.BreweryID == breweryID && W.Name == beerName).ToListAsync();
        }

        public async Task<IEnumerable<Beer>> GetBeersByIdsAsync(IEnumerable<int> beerIds)
        {
            return await _context.Set<Beer>().Where(W => beerIds.Contains(W.BeerID)).ToListAsync();
        }

        public async Task AddBeerAsync(Beer beer)
        {
            await _context.Database.ExecuteSqlRawAsync(
               @"INSERT INTO Beer (Name, AlcoholContent, Price, BreweryID)  VALUES ({0}, {1}, {2}, {3})",
               beer.Name, beer.AlcoholContent, beer.Price, beer.BreweryID);
        }

        public async Task DeleteBeerAsync(Beer beer)
        {
            _context.Remove<Beer>(beer);
            await _context.SaveChangesAsync();
        }
    }
}
