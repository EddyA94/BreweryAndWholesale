using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IWholesalerStockService
    {
        Task<WholesalerStock> GetWholeSalerStockByStockIdAsync(int WholesalerStockId);
        IAsyncEnumerable<WholesalerStock> GetStockByWholesalerIdAndBeerIdAsync(int wholesalerId, List<int> beerId);
        Task UpsertWholesaleStockAsync(WholesalerStock wholesalerStock);
    }
}
