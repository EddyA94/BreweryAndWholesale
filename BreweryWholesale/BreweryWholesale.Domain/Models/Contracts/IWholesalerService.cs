using BreweryWholesale.Domain.Models.DBO;
using BreweryWholesale.Domain.Models.DTO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface IWholesalerService
    {
        Task UpdateWholesalerStockQuantityAsync(WholesalerStock_Dto wholesalerStock_Dto);
    }
}
