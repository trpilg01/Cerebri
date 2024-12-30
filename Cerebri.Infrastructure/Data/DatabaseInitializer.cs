using Cerebri.Domain.Entities;
using Cerebri.Domain.Enums;
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
        public static string InitializeDatabase(IHost host)
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

            SeedData(dbContext);

            return defaultConnectionString;
        }

        public static void SeedData(AppDbContext context)
        {
            if (!context.Moods.Any())
            {
                context.Moods.AddRange(
                    new MoodModel { Name = "Enraged", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Panicked", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Stressed", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Jittery", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Shocked", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Livid", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Furious", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Frustrated", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Tense", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Stunned", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Fuming", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Frightened", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Angry", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Nervous", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Restless", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Anxious", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Apprehensive", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Worried", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Irritated", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Annoyed", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Repulsed", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Troubled", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Concerned", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Uneasy", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Peeved", Type = eMoodType.HighEnergyNegative },
                    new MoodModel { Name = "Stunned", Type = eMoodType.HighEnergyNegative },

                    new MoodModel { Name = "Disgusted", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Glum", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Disappointed", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Down", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Apathetic", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Pessimistic", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Morose", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Discouraged", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Sad", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Bored", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Alienated", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Miserable", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Lonely", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Disheartened", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Tired", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Despondent", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Depressed", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Sullen", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Exhausted", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Fatigued", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Despairing", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Hopeless", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Desolate", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Spent", Type = eMoodType.LowEnergyNegative },
                    new MoodModel { Name = "Drained", Type = eMoodType.LowEnergyNegative },

                    new MoodModel { Name = "Surprised", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Upbeat", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Festive", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Exhilarated", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Ecstatic", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Hyper", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Cheerful", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Motivated", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Inspired", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Elated", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Energized", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Lively", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Excited", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Optimistic", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Enthusiastic", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Pleased", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Focused", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Happy", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Proud", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Thrilled", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Pleasant", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Joyful", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Hopeful", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Playful", Type = eMoodType.HighEnergyPositive },
                    new MoodModel { Name = "Blissful", Type = eMoodType.HighEnergyPositive },

                    new MoodModel { Name = "At Ease", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Easygoing", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Content", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Loving", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Fulfilled", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Calm", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Secure", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Satisfied", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Grateful", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Touched", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Relaxed", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Chill", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Restful", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Blessed", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Balanced", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Mellow", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Thoughtful", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Peaceful", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Comfortable", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Carefree", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Sleepy", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Complacent", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Tranquill", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Cozy", Type = eMoodType.LowEnergyPositive },
                    new MoodModel { Name = "Serene", Type = eMoodType.LowEnergyPositive }
                );
                context.SaveChanges();
            }
        }
    }
}
