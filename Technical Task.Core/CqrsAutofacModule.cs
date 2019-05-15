using System.Reflection;
using Autofac;
using MediatR;
using Technical_Task.Data;

namespace Technical_Task.Core
{
    public class CqrsAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            this.Register(builder);
        }

        private void Register(ContainerBuilder builder)
        {
            builder.Register(c => new DesignTimeDbContextFactory().CreateDbContext(new string[1])).As<ApplicationDbContext>().InstancePerLifetimeScope();

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterAssemblyTypes(typeof(CqrsAutofacModule).GetTypeInfo().Assembly).AsImplementedInterfaces();
        }
    }
}
