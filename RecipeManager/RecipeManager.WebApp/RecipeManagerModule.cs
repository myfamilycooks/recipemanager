using Autofac;
using RecipeManager.WebApp.Entities;
using RecipeManager.WebApp.Services;

namespace RecipeManager.WebApp
{
    public class RecipeManagerModule : Module
    {
        public string ConnectionString { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Database>().As<IDatabase>().WithParameter("connectionString", ConnectionString);
            builder.RegisterType<RecipeRepository>().As<IDataRepository<Recipe>>();
        }
    }
}