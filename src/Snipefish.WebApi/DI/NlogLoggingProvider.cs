using NLog;
using NLog.Extensions.Logging;

namespace Snipefish.WebApi.DI
{
    public static class NlogLoggingProvider
    {
        public static void AddNlogLogging(this IServiceCollection services, IConfiguration configs)
        {
            LogManager.Configuration = new NLogLoggingConfiguration(configs.GetSection("NLog"));
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.AddNLog();
            });
        }
    }
}
