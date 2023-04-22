using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly BrewerWholesaleDBContext _context;

        public SaleRepository(BrewerWholesaleDBContext context)
        {
            _context = context;
        }

        public async Task UpsertSalesToWholeSalerAsync(Sale sale)
        {
            _context.Add<Sale>(sale);
            await _context.SaveChangesAsync();
        }
    }
}
