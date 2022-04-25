using Autofac;

namespace Snipefish.WebApi.DI
{
    public static class AutofacConfigs
    {
        public static IHostBuilder ConfigAutofacContainer(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule<PersistenceModule>();
                builder.RegisterModule<ApplicationModule>();
            });
        }
    }
}
