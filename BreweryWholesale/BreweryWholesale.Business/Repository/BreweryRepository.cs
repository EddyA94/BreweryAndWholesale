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

        public async Task<Brewery> GetAllBeersByBreweryNameAsync(string breweryName)
        {
            var brewery = await GetBreweryByNameAsync(breweryName);
            var res = await _context.Set<Brewery>().Include(a => a.Beers).Where(W => W.BrewerID == brewery.BrewerID).FirstAsync();
            if (res.Beers.Count == 0)
            {
                throw new CustomExceptions("Brewery Do not have any beers", (int)System.Net.HttpStatusCode.NotFound);
            }
            return res;
        }

        public async Task<Brewery> GetBreweryByNameAsync(string breweryName)
        {
            var res = await _context.Set<Brewery>().Where(W => W.Name == breweryName).FirstOrDefaultAsync();
            if (res == null)
            {
                throw new CustomExceptions("Brewery Does not Exist", (int)System.Net.HttpStatusCode.NotFound);
            }
            return res;
        }
    }
}
