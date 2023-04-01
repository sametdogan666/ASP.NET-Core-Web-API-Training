using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Interceptors;
using Entities;

namespace InvocationApp;

public class DefaultModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var proxyOptions = new ProxyGenerationOptions
        {
            Selector = new AspectInterceptorSelector()
        };

        builder.RegisterType<Employee>()
            .As<IEmployee>()
            .EnableInterfaceInterceptors(proxyOptions)
            .SingleInstance();
    }
}

