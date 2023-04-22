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

        public async Task<WholesalerStock?> GetWholeSalerStockByIdandBeerIdAsync(int wholesalerId, int beerId)
        {
            try
            {
                return await _context.Set<WholesalerStock>().Where(W => W.WholesalerID == wholesalerId && W.BeerID == beerId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<WholesalerStock?> GetWholeSalerStockByStockIdAsync(int WholesalerStockId)
        {
            try
            {
                return await _context.Set<WholesalerStock>().Where(W => W.WholesalerStockID == WholesalerStockId).FirstOrDefaultAsync();
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
                var existingStock = await _context.WholesalerStock.FirstOrDefaultAsync(e => e.WholesalerStockID == wholesalerStock.WholesalerStockID);
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
