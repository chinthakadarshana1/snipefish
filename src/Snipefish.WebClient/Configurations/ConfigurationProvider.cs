namespace Snipefish.WebClient.Configurations
{
    public static class ConfigurationProvider
    {
        public static void AddApplicationConfigurations(this IServiceCollection services, IConfiguration configs)
        {
            var snipefishWebConfiguration = configs.GetSection("SnipefishConfigs").Get<SnipefishWebConfiguration>();

            services.AddSingleton(typeof(SnipefishWebConfiguration),
                snipefishWebConfiguration);

            services.AddSingleton(typeof(SnipefishWebApi),
                new SnipefishWebApi(snipefishWebConfiguration));
        }
    }
}
