using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Street.Healing.API.Controllers;
using Street.Healing.Business.Tech.Helpers;

namespace Street.Healing.API.Middlewares
{
    public class JWTTokenMiddleware(ILogger<JWTTokenMiddleware> logger) : IMiddleware
    {
     
        private const string APIKEY = "XApiKey";
        private readonly ILogger<JWTTokenMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context , RequestDelegate next)
        {
            try
            {
                if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync(ConstMessages.ApiKeyNotProvided);
                    return;
                }
                var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
                var apiKey = appSettings.GetValue<string>(APIKEY) ?? string.Empty;
                if (!apiKey.Equals(extractedApiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync(ConstMessages.UnauthorizedClient);
                    return;
                }
                await next(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"{ConstMessages.ExInvkAsyncJWT} {ex.Message} ");
               
            }
        }

    }
}
