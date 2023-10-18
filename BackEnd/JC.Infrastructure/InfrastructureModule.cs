using JC.Core.Repositories;
using JC.Infrastructure.Persistence.Repositories;
using JC.Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace JC.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string conexao)
        {
            services
                .AddDbSession(conexao)
                .AddRepositories();
            //.AddMessageBus();

            return services;
        }

        public static IServiceCollection AddDbSession(this IServiceCollection services, string _con)
        {
            services.AddScoped<DbSession>(x =>
            {
                return new DbSession(_con);
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IPermissaoRepository, PermissaoRepository>();

            return services;
        }

        //private static IServiceCollection AddMessageBus(this IServiceCollection services)
        //{
        //    services.AddScoped<IMessageBusService, RabbitMqService>();

        //    return services;
        //}
    }
}
