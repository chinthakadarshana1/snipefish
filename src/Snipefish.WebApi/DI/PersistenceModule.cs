using Autofac;

namespace Snipefish.WebApi.DI
{
    public class PersistenceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Snipefish.Persistence.Mongo.Repositories.Command.TodoCommandRepository).Assembly)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
