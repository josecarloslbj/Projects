using FluentMigrator.Runner;

namespace JC.WebApi;

public static class MySQLMigrationModule
{
    public static void AddMigration(WebApplicationBuilder builder, string connection)
    {
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
}
