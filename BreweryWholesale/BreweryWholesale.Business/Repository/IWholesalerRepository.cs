namespace BreweryWholesale.Infrastructure.Repository
{
    public interface IWholesalerRepository
    {
        Task<bool> IsWholesalerAvailableAsync(int wholesalerId);
    }
}
