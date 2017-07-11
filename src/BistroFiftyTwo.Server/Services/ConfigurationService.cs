using Microsoft.Extensions.Configuration;

namespace BistroFiftyTwo.Server.Services
{
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