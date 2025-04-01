using CodingTracker;
using CodingTracker.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


        // 1️⃣ Build Configuration (Reads appsettings.json)
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // 2️⃣ Setup Dependency Injection
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)  // Register configuration
            .AddSingleton<ConfigService>()                // Register ConfigService
            .AddScoped<CodingTrackerRepository>()         // Register Repository
            .AddScoped<Menu>()                            // Register Menu class
            .BuildServiceProvider();

        // 3️⃣ Get Required Services
        var repository = serviceProvider.GetRequiredService<CodingTrackerRepository>();
        repository.InitialiseDatabase(); // Initialize DB

        var menu = serviceProvider.GetRequiredService<Menu>();
        menu.DisplayMenu(); // Start the Menu
