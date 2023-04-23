using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IWholesalerStockService
    {
        Task<WholesalerStock> GetWholeSalerStockByStockIdAsync(int WholesalerStockId);
        Task<IEnumerable<WholesalerStock>> GetStockByWholesalerIdAndBeerIdAsync(int wholesalerId, List<int> BeerId);
        Task UpsertWholesaleStockAsync(WholesalerStock wholesalerStock);
    }
}
