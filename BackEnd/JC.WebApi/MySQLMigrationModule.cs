using FluentMigrator.Runner;
using MySql.Data.MySqlClient;

namespace JC.WebApi;

public static class MySQLMigrationModule
{
    public static void AddMigration(WebApplicationBuilder builder, string connection)
    {
        CreateSchemaDatabase();

        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString(connection)
                .ScanIn(typeof(JC.MySQLMigration.MigrationsAssembly).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

       
    }

    public static void AddMigration(WebApplication app)
    {
        // Put the database update into a scope to ensure
        // that all resources will be disposed.
        using (var scope = app.Services.CreateScope())
        {
            UpdateDatabase(scope.ServiceProvider);
        }
    }

    /// <summary>
    /// UpdateDatabase
    /// </summary>
    /// <param name="serviceProvider"></param>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }

    static void CreateSchemaDatabase()
    {
        Console.Write("Connecting to MYSQL Server ... ");
        MySqlConnectionStringBuilder builderMySQL = new MySqlConnectionStringBuilder();
        builderMySQL.Server = "localhost";
        builderMySQL.UserID = "root";
        builderMySQL.Password = "root";
        builderMySQL.Port = 3306;

        using (MySqlConnection con = new MySqlConnection(builderMySQL.ToString()))
        {
            con.Open();
            Console.Write("Connected in MYSQL Server ... ");

            string sql = @"SELECT SCHEMA_NAME
                          FROM INFORMATION_SCHEMA.SCHEMATA
                         WHERE SCHEMA_NAME = 'storeShop'";

            using (MySqlCommand command = new MySqlCommand(sql, con))
            {
                var SCHEMA_NAME = command.ExecuteScalar();
                if (SCHEMA_NAME == null)
                {
                    Console.Write("Creating DATABASE in MYSQL Server ... ");

                    String sqlCreate = "CREATE DATABASE `storeShop`";
                    using (MySqlCommand commandCreate = new MySqlCommand(sqlCreate, con))
                    {
                        commandCreate.ExecuteNonQuery();
                        Console.WriteLine("Created DATABASE.");
                    }
                }
            }

        }
    }
}
