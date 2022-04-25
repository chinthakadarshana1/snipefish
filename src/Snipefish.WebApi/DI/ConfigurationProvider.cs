using Snipefish.Persistence.Mongo.Configurations;

namespace Snipefish.WebApi.DI
{
    public static class ConfigurationProvider
    {
        public static void AddApplicationConfigurations(this IServiceCollection services, IConfiguration configs)
        {
            services.AddSingleton(typeof(SnipefishDbConfiguration),
                configs.GetSection("MongoConfigurations:SnipefishDbConfiguration").Get<SnipefishDbConfiguration>());

        }
    }
}
