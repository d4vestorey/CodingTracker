using Microsoft.Data.Sqlite;

namespace CodingTracker.Repository
{
    public class CodingTrackerRepository
    {
        readonly ConfigService _configService = new ConfigService();

        public CodingTrackerRepository()
        {
            InitialiseDatabase();
        }

        private void InitialiseDatabase()
        {
            if (!File.Exists(_configService.GetDbPath()))
            {
                Console.WriteLine("Database not found. Creating new database...");
                //Simply opening a connection to a non-existent SQLite file automatically creates it.

                try
                {
                    using (var connection = new SqliteConnection(_configService.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        string createTableQuery = @"CREATE TABLE IF NOT EXISTS CodingTracker (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Start DATETIME,
                                            End DATETIME, 
                                            Duration INTEGER NOT NULL)";
                        using (var command = new SqliteCommand(createTableQuery, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    Console.WriteLine("Database created successfully.");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating database: {ex.Message}");
                    Console.ReadLine();

                }
            }
            else
            {
                Console.WriteLine("Database found. Proceeding...");
                Console.ReadLine();
            }
        }

    }
}