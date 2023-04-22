using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository
{
    public interface IWholesalerStockRepository
    {
        Task<WholesalerStock?> GetWholeSalerStockByIdandBeerIdAsync(int wholesalerId, int beerId);

        Task UpsertWholeSalerStockAsync(WholesalerStock wholesalerStock);

        Task<WholesalerStock?> GetWholeSalerStockByStockId(int WholesalerStockId);
    }
}
