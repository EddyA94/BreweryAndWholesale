using BreweryWholesale.Domain.Models.Contracts;

namespace BreweryWholesale.Api.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public AuthenticationMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsLoginEndpoint(context))
            {
                // Skip authentication for the login API
                await _next(context);
                return;
            }

            // Perform authentication logic here
            if (!await IsAuthenticated(context))
            {
                // Return unauthorized response
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            // User is authenticated, proceed with the next middleware
            await _next(context);
        }

        private bool IsLoginEndpoint(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/api/User/LoginUser");
        }

        private async Task<bool> IsAuthenticated(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return false;
            }
            if (await _tokenService.IsValid(token))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
