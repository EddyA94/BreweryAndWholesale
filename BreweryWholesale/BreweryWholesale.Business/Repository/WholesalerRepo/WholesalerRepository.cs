using BreweryWholesale.Domain.Models.DBO;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Infrastructure.Repository.WholesalerRepo
{
    public class WholesalerRepository : IWholesalerRepository
    {
        private readonly BrewerWholesaleDBContext _context;

        public WholesalerRepository(BrewerWholesaleDBContext context)
        {
            _context = context;
        }

        public async Task<bool> IsWholesalerAvailableAsync(int wholesalerId)
        {
            return await _context.Set<Wholesaler>().AnyAsync(W => W.WholesalerID == wholesalerId);
        }
    }
}
