using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SqlClient;


namespace Cerebri.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var databaseName = configuration["DatabaseSettings:DatabaseName"];
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception("Database name is not provided in the configuration");
            }

            var serverConnection = configuration.GetConnectionString("ServerConnection");

            using var connection = new SqlConnection(serverConnection);
            connection.Open();

            // Check if the database exists
            var commandText = $@"
                IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}')
                BEGIN
                    CREATE DATABASE [{databaseName}];
                END";
            using var command = new SqlCommand(commandText, connection);
            command.ExecuteNonQuery();

            var defaultConnectionString = new SqlConnectionStringBuilder(serverConnection)
            {
                InitialCatalog = databaseName
            }.ConnectionString;

            configuration["ConnectionStrings:DefaultConnection"] = defaultConnectionString;
            dbContext.Database.SetConnectionString(defaultConnectionString);

            dbContext.Database.Migrate();
        }
    }
}
