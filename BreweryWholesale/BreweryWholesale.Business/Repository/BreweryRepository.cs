using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Infrastructure.Repository
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly BrewerWholesaleDBContext _context;

        public BreweryRepository(BrewerWholesaleDBContext context)
        {
            _context = context;
        }

        public async Task<Brewery?> GetAllBeersByBreweryNameAsync(string breweryName)
        {
            var brewery = await GetBreweryByNameAsync(breweryName);
            var res = await _context.Set<Brewery>().Include(a => a.Beers).Where(W => W.BrewerID == brewery.BrewerID).FirstOrDefaultAsync();         
            return res;
        }

        public async Task<Brewery> GetBreweryByNameAsync(string breweryName)
        {
            var res = await _context.Set<Brewery>().Where(W => W.Name == breweryName).FirstOrDefaultAsync();
            return res ?? throw new CustomExceptions("Brewery Does not Exist", (int)System.Net.HttpStatusCode.NotFound);
        }
    }
}
