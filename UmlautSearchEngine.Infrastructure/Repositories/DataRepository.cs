using Microsoft.Data.Sqlite;
using UmlautSearchEngine.Application.Interfaces;

namespace UmlautSearchEngine.Infrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<string> Search(string sql)
        {
            var results = new List<string>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = sql;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                results.Add(reader["lastname"].ToString());
            }

            return results;
        }

        public void SaveVariants(string originalName, IEnumerable<string> variants)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            foreach (var variant in variants)
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    "INSERT OR IGNORE INTO tbl_phonebook (lastname) VALUES (@variant)";

                command.Parameters.AddWithValue("@variant", variant);

                command.ExecuteNonQuery();
            }
        }
    }
}

