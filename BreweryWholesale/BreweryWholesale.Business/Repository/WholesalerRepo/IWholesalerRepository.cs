namespace BreweryWholesale.Infrastructure.Repository.WholesalerRepo
{
    public interface IWholesalerRepository
    {
        Task<bool> IsWholesalerAvailableAsync(int wholesalerId);
    }
}
