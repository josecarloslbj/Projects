using JC.Infrastructure.Shared;
using JC.WebApi.Middlewares;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);
string _con = builder.Configuration["ConnectionStrings:StoreShop"].ToString();

var services = builder.Services;

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new JC.WebApi.Filters.RespostaBaseFilter());

}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

services.AddScoped<DbSession>(x =>
{
    return new DbSession(_con);
});

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<DbTransactionMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();


