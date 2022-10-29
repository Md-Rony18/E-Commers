using Autofac;
using OnlineShop.Core.IConfigration;
using OnlineShop.Data;

namespace OnlineShop
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnityOfWork>().As<IUnityOfWork>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
