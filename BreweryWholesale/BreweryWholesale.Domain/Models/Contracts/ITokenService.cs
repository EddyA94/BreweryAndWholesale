namespace BreweryWholesale.Domain.Models.Contracts
{
    public interface ITokenService
    {
        Task<bool> IsValid(string token);
    }
}
