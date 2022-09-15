using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence
{
    static class Configuration
    {
        static public string connectionString
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                //appsettingsten connection string alacağız
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ECommerceAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("DBConnectionString");
            }
        }
    }
}
