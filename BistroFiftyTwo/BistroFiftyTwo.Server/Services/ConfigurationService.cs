using Microsoft.Extensions.Configuration;

namespace BistroFiftyTwo.Server.Services
{
    public interface IConfigurationService
    {
        string Get(string key);
    }

    public class ConfigurationService : IConfigurationService
    {
        public ConfigurationService(IConfigurationRoot root)
        {
            ConfigurationRoot = root;
        }

        protected IConfigurationRoot ConfigurationRoot { get; set; }

        public string Get(string key)
        {
            return ConfigurationRoot[key];
        }
    }
}