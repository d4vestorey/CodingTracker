 using Microsoft.Extensions.Configuration;

namespace CodingTracker
{
   
    public class ConfigService
    {
        private readonly IConfiguration _configuration;

        // Inject IConfiguration via constructor
        public ConfigService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetConnectionString(string key)
        {
            return _configuration.GetConnectionString(key)
                ?? throw new InvalidOperationException($"Connection string '{key}' is not configured.");
        }

        public string GetDbPath()
        {
            return _configuration["DatabasePaths:LocalDatabase"]
                ?? throw new InvalidOperationException("Database path is not configured.");
        }
    }

}