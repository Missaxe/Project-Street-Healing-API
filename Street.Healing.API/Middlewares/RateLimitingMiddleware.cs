using Street.Healing.Business.Tech.Helpers;
using System.Collections.Concurrent;

namespace Street.Healing.API.Middlewares
{
    public class RateLimitingMiddleware(ILogger<RateLimitingMiddleware> logger) : IMiddleware
    {
        private static readonly ConcurrentDictionary<string, RequestLog> _requestLogs = new();
        private readonly ILogger<RateLimitingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context , RequestDelegate next)
        {
            try
            {
                var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? String.Empty;
                var now = DateTime.UtcNow;
                var requestLog = _requestLogs.GetOrAdd(ipAddress, new RequestLog());
                lock (requestLog)
                {
                    requestLog.Requests.Enqueue(now);
                    while (requestLog.Requests.Count > 0 && (now - requestLog.Requests.Peek()).TotalSeconds > 60)
                    {
                        requestLog.Requests.Dequeue();
                    }
                    if (requestLog.Requests.Count > 10)
                    {
                        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                        context.Response.WriteAsync(ConstMessages.RateLimitExceeded);
                        return;
                    }
                }
                await next(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(message: $"{ConstMessages.ExInvkAsyncRateLimit} {ex.Message} ");

            }
        }
    }
    public class RequestLog
    {
        public Queue<DateTime> Requests { get; } = new();
    }
}

