using BreweryWholesale.Domain.Models.DBO;

namespace BreweryWholesale.Infrastructure.Repository
{
    public interface ISaleRepository
    {
        Task UpsertSalesToWholeSalerAsync(Sale sale);
    }
}
