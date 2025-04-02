using CodingTracker.Models;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Repository
{
    public class CodingTrackerRepository
    {
        private readonly ConfigService _configService;

        public CodingTrackerRepository(ConfigService configService)
        {
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
        }


        internal List<CodingSession> GetSessions()
        {
            var sessions = new List<CodingSession>();
            using (var connection = new SqliteConnection(_configService.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                string query = "SELECT * FROM CodingTracker";
                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sessions.Add(new CodingSession(
                            Convert.ToInt32(reader["Id"]),
                            Convert.ToDateTime(reader["Start"]),
                            Convert.ToDateTime(reader["End"])
                        ));
                    }

                }
            }
            return sessions;
        }


        internal void AddSession(CodingSession session)
        {
            using (var connection = new SqliteConnection(_configService.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                string query = "INSERT INTO CodingTracker (Start, End, Duration) VALUES (@start, @end, @duration)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@start", session.StartTime);
                    command.Parameters.AddWithValue("@end", session.EndTime);
                    command.Parameters.AddWithValue("@duration", session.Duration);
                    command.ExecuteNonQuery();
                }
            }
        }

        internal void DeleteSession(int id)
        {
            using(var connection = new SqliteConnection(_configService.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                string query = "DELETE FROM CodingTracker WHERE Id = @id";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id); 
                    command.ExecuteNonQuery();
                }
            }
        }


        internal void InitialiseDatabase()
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