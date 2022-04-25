using Autofac;
using Snipefish.Application.Commands.Todo;

namespace Snipefish.WebApi.DI
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AddTodoCommand).Assembly)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
