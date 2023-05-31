using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository.SaleRepo
{
    public interface ISaleRepository
    {
        Task UpsertSalesToWholeSalerAsync(Sale sale);
    }
}
