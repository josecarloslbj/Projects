using JC.Infrastructure.Shared;
using JC.WebApi.Middlewares;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JC.Infrastructure;
using JC.Application;
using JC.WebApi;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
string _con = builder.Configuration["ConnectionStrings:StoreShop"].ToString();

var services = builder.Services;


try
{
    Console.Write("Connecting to MYSQL Server ... ");
    MySqlConnectionStringBuilder builderMySQL = new MySqlConnectionStringBuilder();
    builderMySQL.Server = "localhost";
    builderMySQL.UserID = "root";
    builderMySQL.Password = "root";
    builderMySQL.Port = 3306;

    using (MySqlConnection connection = new MySqlConnection(builderMySQL.ToString()))
    {
        connection.Open();
        Console.Write("Connected in MYSQL Server ... ");

        string sql = @"SELECT SCHEMA_NAME
                          FROM INFORMATION_SCHEMA.SCHEMATA
                         WHERE SCHEMA_NAME = 'storeShop'";

        using (MySqlCommand command = new MySqlCommand(sql, connection))
        {
            var SCHEMA_NAME = command.ExecuteScalar();
            if (SCHEMA_NAME == null)
            {
                Console.Write("Creating DATABASE in MYSQL Server ... ");

                String sqlCreate = "CREATE DATABASE `storeShop`";
                using (MySqlCommand commandCreate = new MySqlCommand(sqlCreate, connection))
                {
                    commandCreate.ExecuteNonQuery();
                    Console.WriteLine("Created DATABASE.");
                }
            }
        }

    }
}
catch (Exception ex)
{
    Console.WriteLine("Erro ao verificar Banco de dados MySQL" + ex.Message);
    throw;
}


//try
//{
//    var options =
//        new MySqlStorageOptions
//        {
//            TransactionIsolationLevel = IsolationLevel.ReadCommitted,
//            QueuePollInterval = TimeSpan.FromSeconds(15),
//            JobExpirationCheckInterval = TimeSpan.FromHours(1),
//            CountersAggregateInterval = TimeSpan.FromMinutes(5),
//            PrepareSchemaIfNecessary = true,
//            DashboardJobListLimit = 50000,
//            TransactionTimeout = TimeSpan.FromMinutes(1),
//            TablesPrefix = "Hangfire"
//        };
//    var storage = new MySqlStorage(_con, options);
//    services.AddHangfire(config => config.UseStorage(storage));
//    services.AddHangfireServer();
//}
//catch (Exception ex)
//{
//    throw new Exception($"Não foi possível conectar ao banco MySQL. Erro:{ex.Message}");
//}

// Add services to the container.

services.AddControllers(options =>
{
    options.Filters.Add(new JC.WebApi.Filters.RespostaBaseFilter());

}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});


services
    .AddInfrastructure(_con)
    .AddApplication();

MySQLMigrationModule.AddMigration(builder, _con);

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<DbTransactionMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

MySQLMigrationModule.AddMigration(app);

app.Run();


