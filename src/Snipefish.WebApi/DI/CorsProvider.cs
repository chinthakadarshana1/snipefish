namespace Snipefish.WebApi.DI
{
    public static class CorsProvider
    {
        public const string SnipefishCors = "SnipefishCors";

        public static void AddCorsConfigurations(this IServiceCollection services, IConfiguration configs)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: SnipefishCors,
                    policy =>
                    {
                        policy
                            //.WithOrigins("http://example.com","http://www.contoso.com")
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

        }
    }
}
