using BreweryWholesale.Domain.Models.DBO;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Infrastructure.Repository
{
    public class WholesalerStockRepository : IWholesalerStockRepository
    {
        private readonly BrewerWholesaleDBContext _context;

        public WholesalerStockRepository(BrewerWholesaleDBContext context)
        {
            _context = context;
        }

    
        public async IAsyncEnumerable<WholesalerStock> GetWholeSalerStockByIdandBeerIdAsync(int wholesalerId, List<int> beerId)
        {
            await foreach (WholesalerStock wholesalerStock in _context.Set<WholesalerStock>().Where(W => W.WholesalerID == wholesalerId && beerId.Contains(W.BeerID)).AsAsyncEnumerable())
            {
                yield return wholesalerStock;
            }
        }

        public async Task<WholesalerStock?> GetWholeSalerStockByStockIdAsync(int WholesalerStockId)
        {
            try
            {
                return await _context.Set<WholesalerStock>().FindAsync(WholesalerStockId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpsertWholeSalerStockAsync(WholesalerStock wholesalerStock)
        {
            try
            {
                var existingStock = await _context.WholesalerStock.FindAsync(wholesalerStock.WholesalerStockID);
                if (existingStock == null)
                {
                    _context.Add(wholesalerStock);
                }
                else
                {
                    _context.Entry(existingStock).CurrentValues.SetValues(wholesalerStock);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
