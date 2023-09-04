using System;
using System.Data;
using System.Data.SqlClient;

namespace InsecureCode.Infrastructure
{
    public class ConnectionUtil_Pooling : IDisposable
    {
        private static readonly Lazy<SqlConnection> instance = new Lazy<SqlConnection>(CreateConnection);

        public static SqlConnection GetConnection()
        {
            return instance.Value;
        }

        private static SqlConnection CreateConnection()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("AppDbContext");

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(connectionString);
            sqlBuilder.Pooling = true;
            sqlBuilder.MinPoolSize = 1;
            sqlBuilder.MaxPoolSize = 10;

            return new SqlConnection(sqlBuilder.ConnectionString);
        }

        public void Dispose()
        {
            if (instance.IsValueCreated)
            {
                instance.Value.Close();
                instance.Value.Dispose();
            }
        }

    }

}
