namespace BreweryWholesale.Api.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
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
            if (!IsAuthenticated(context))
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
            return context.Request.Path.StartsWithSegments("/api/login");
        }

        private bool IsAuthenticated(HttpContext context)
        {
            // Perform your authentication logic here
            // You can access the request headers, tokens, cookies, etc. to validate the authentication

            // Return true if the user is authenticated; otherwise, return false
            // For demonstration purposes, let's assume all requests are authenticated
            return true;
        }
    }
}
