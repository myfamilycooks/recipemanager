using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace BistroFiftyTwo.Server
{
    public class BistroFiftyTwoServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(ThisAssembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(ThisAssembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
        }
    }
}
