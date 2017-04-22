using System.Reflection;
using Autofac;
using RecipeManager.WebApp.Data;
using RecipeManager.WebApp.Entities;
using RecipeManager.WebApp.Services;
using Module = Autofac.Module;

namespace RecipeManager.WebApp
{
    public class RecipeManagerModule : Module
    {
        public string ConnectionString { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Database>().As<IDatabase>().WithParameter("connectionString", ConnectionString);
   
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}