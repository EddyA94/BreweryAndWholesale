namespace BreweryWholesale.Infrastructure.Repository.TokenRepo
{
    public interface ITokenRepository
    {
        Task StoreToken(string userId, string token);
        Task<string> GetToken(string userId);
        Task<bool> IsValidToken(string token);
        Task RemoveToken(string userId);
    }
}
