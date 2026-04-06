using Microsoft.Data.Sqlite;

namespace UmlautSearchEngine.Infrastructure.Database
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InitializeAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            // Create table if not exists
            var createTableCmd = new SqliteCommand(@"
            CREATE TABLE IF NOT EXISTS tbl_phonebook (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                lastname TEXT NOT NULL
            )", connection);

            await createTableCmd.ExecuteNonQueryAsync();

            var createIndexCmd = new SqliteCommand(@"
             CREATE INDEX IF NOT EXISTS idx_lastname 
              ON tbl_phonebook(lastname);
            ", connection);

            await createIndexCmd.ExecuteNonQueryAsync();
        }
    }
}
