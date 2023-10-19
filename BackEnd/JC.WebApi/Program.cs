using JC.Application;
using JC.Core.DapperMapping;
using JC.Infrastructure;
using JC.WebApi;
using JC.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);
string _con = builder.Configuration["ConnectionStrings:StoreShop"].ToString();

var services = builder.Services;

services.AddCors();

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
    .AddApplication()
    .AddInfrastructure(_con);
    

RegisterMappings.Register();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
MySQLMigrationModule.AddMigration(builder, _con);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()

    //.AllowCredentials()
    );


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<DbTransactionMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

MySQLMigrationModule.AddMigration(app);

app.Run();


