using BreweryWholesale.Infrastructure.Repository.UserRepo;
using BreweryWholesale.Domain.Models.Contracts;
using BreweryWholesale.Domain.Models.DTO;
using BreweryWholesale.Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using BreweryWholesale.Infrastructure.Utilities;
using BreweryWholesale.Infrastructure.Repository.TokenRepo;

namespace BreweryWholesale.Infrastructure.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;

        public UserServices(IUserRepository userRepository, IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> LoginUser(User_Dto user_Dto)
        {
            if (string.IsNullOrWhiteSpace(user_Dto.UserName) || string.IsNullOrWhiteSpace(user_Dto.Password))
            {
                throw new CustomExceptions("User Name and Password Cannot be Empty", (int)System.Net.HttpStatusCode.BadRequest);
            }

            var user = await _userRepository.GetUserByUserName(user_Dto.UserName);
            if (user is null || user.Password is null)
            {
                throw new CustomExceptions("Username or Password Is Incorrect", (int)System.Net.HttpStatusCode.NotFound);
            }

            bool isPasswordValid = PasswordHasher.VerifyPassword(user_Dto.Password, user.Password);

            if (isPasswordValid)
            {
                var tokenGenerator = new TokenGenerator(_configuration);
                var token = tokenGenerator.GenerateToken(user_Dto.UserName);
                await _tokenRepository.StoreToken(user_Dto.UserName, token);
                // Do something with the generated token
                return token;
            }
            else
            {
                // Handle incorrect password
                throw new CustomExceptions("Username or Password Is Incorrect", (int)System.Net.HttpStatusCode.NotFound);
            }
        }

    }
}
