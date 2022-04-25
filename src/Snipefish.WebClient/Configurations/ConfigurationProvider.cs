namespace Snipefish.WebClient.Configurations
{
    public static class ConfigurationProvider
    {
        public static void AddApplicationConfigurations(this IServiceCollection services, IConfiguration configs)
        {
            services.AddSingleton(typeof(SnipefishWebConfiguration),
                configs.GetSection("SnipefishConfigs").Get<SnipefishWebConfiguration>());

        }
    }
}
