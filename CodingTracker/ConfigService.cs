using Microsoft.Extensions.Configuration;

namespace CodingTracker
{
    public class ConfigService
    {
        private readonly IConfiguration _configuration;

        public ConfigService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensures it reads from the correct path
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string GetConnectionString(string key)
        {
            return _configuration.GetConnectionString(key)
            ?? throw new InvalidOperationException("Connection string is not configured.");
        }

        public string GetDbPath()
        {
            return _configuration["DatabasePaths:LocalDatabase"] 
            ?? throw new InvalidOperationException("Database path is not configured.");
        }

    }
}