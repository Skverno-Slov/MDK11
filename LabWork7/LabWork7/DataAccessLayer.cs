using Microsoft.Data.SqlClient;

namespace LabWork7
{
    public static class DataAccessLayer
    {
        const string SuccessSettingsChange = "Замена настроек успешно выполнена.";
        const string ConnectionInProcess = "Идет подключение";

        static string _server = "mssql";
        static string _database = "MsSQL";
        static string _login = "ispp3114";
        static string _password = "3114";

        public static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder builder = new()
                {
                    DataSource = _server,
                    InitialCatalog = _database,
                    UserID = _login,
                    Password = _password,
                    TrustServerCertificate = true
                };
                return builder.ConnectionString;
            }
        }

        public static void ChangeSettings(string server, string database, string login, string password)
        {
            _server = server;
            _database = database;
            _login = login;
            _password = password;

            Console.WriteLine(SuccessSettingsChange);
        }

        public static bool TryOpenConnection()
        {
            using SqlConnection connection = new(ConnectionString);
            Console.WriteLine(ConnectionInProcess);
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<int> ExecuteSqlAsync(string command)
        {
            using SqlConnection connection = new(ConnectionString);
            Console.WriteLine(ConnectionInProcess);
            await connection.OpenAsync();

            SqlCommand sqlCommand = new(command, connection);
            return await sqlCommand.ExecuteNonQueryAsync();
        }

        public static async Task<object?> GetObjectAsync(string command)
        {
            using SqlConnection connection = new(ConnectionString);
            Console.WriteLine(ConnectionInProcess);
            await connection.OpenAsync();

            SqlCommand sqlCommand = new(command, connection);
            return await sqlCommand.ExecuteScalarAsync();
        }

        public static async Task СhangePriceAsync(decimal price)
        {
            string command = "UPDATE Game SET Price += @price;";

            using SqlConnection connection = new(ConnectionString);
            Console.WriteLine(ConnectionInProcess);
            await connection.OpenAsync();

            SqlCommand sqlCommand = new(command, connection);
            sqlCommand.Parameters.AddWithValue("@price", price);
            await sqlCommand.ExecuteNonQueryAsync();
        }
    }
}
