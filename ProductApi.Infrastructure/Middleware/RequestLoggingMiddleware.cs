
using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ProductApi.Infrastructure.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public RequestLoggingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next)); // Verificación para evitar null
            _logFilePath = configuration["Logging:FilePath"] ?? "request_logs.txt";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var stopwatch = Stopwatch.StartNew();
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                stopwatch.Stop();
                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                var logEntry = $"{context.Request.Method} {context.Request.Path} responded in {elapsedMilliseconds}ms";
                await File.AppendAllTextAsync(_logFilePath, logEntry + "\n");
            }
        }
    }
}
