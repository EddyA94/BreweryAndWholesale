using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Infrastructure.Exceptions;
using BreweryWholesale.Infrastructure.Repository.TokenRepo;

namespace BreweryWholesale.Infrastructure.Services
{
    public class TokenServices : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenServices(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<bool> IsValid(string token)
        {
            if (await _tokenRepository.IsValidToken(token))
            {
                return true;
            }
            else
            {
                throw new CustomExceptions("Unauthorized Access", (int)System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
