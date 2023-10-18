using JC.Application;
using JC.Core.DapperMapping;
using JC.Infrastructure;
using JC.WebApi;
using JC.WebApi.Middlewares;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);
string _con = builder.Configuration["ConnectionStrings:StoreShop"].ToString();

var services = builder.Services;

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

RegisterMappings.Register();
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


