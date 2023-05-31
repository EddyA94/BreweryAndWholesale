using StackExchange.Redis;

namespace BreweryWholesale.Infrastructure.Repository.TokenRepo
{
    public class TokenRepository: ITokenRepository
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public TokenRepository()
        {
            _redisConnection = ConnectionMultiplexer.Connect("127.0.0.1:6379"); // Replace with your Redis connection string
        }

        public async Task StoreToken(string userId, string token)
        {
            var redisDb = _redisConnection.GetDatabase();
            await redisDb.StringSetAsync(userId, token);
        }

        public async Task<string> GetToken(string userId)
        {
            var redisDb = _redisConnection.GetDatabase();
            return await redisDb.StringGetAsync(userId);
        }

        public async Task RemoveToken(string userId)
        {
            var redisDb = _redisConnection.GetDatabase();
            await redisDb.KeyDeleteAsync(userId);
        }
    }
}
