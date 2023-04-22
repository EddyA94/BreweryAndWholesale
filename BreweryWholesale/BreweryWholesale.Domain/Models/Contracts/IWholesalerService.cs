using BreweryWholesale.Domain.Models.DTO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IWholesalerService
    {
        Task<bool> IsWholesalerAvailableAsync(int wholesalerId);
        Task UpdateWholesalerStockQuantityAsync(WholesalerStock_Dto wholesalerStock_Dto);
    }
}