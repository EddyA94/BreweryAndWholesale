using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository.WholesalerRepo
{
    public interface IWholesalerStockRepository
    {
        IAsyncEnumerable<WholesalerStock> GetWholeSalerStockByIdandBeerIdAsync(int wholesalerId, List<int> beerId);

        Task UpsertWholeSalerStockAsync(WholesalerStock wholesalerStock);

        Task<WholesalerStock?> GetWholeSalerStockByStockIdAsync(int WholesalerStockId);
    }
}
