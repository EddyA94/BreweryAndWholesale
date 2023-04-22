using BreweryWholesale.Domain.Models.DTO;

namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface ISaleService
    {
        Task UpsertSalesToWholesalerAsync(Sale_Dto sale_Dto);
    }
}
